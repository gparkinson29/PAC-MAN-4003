using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float xInput, zInput;
    [SerializeField]
    private NavMeshAgent nma;
    [SerializeField]
    private Vector3 movementDirection, currentPosition, lastPosition;
    [SerializeField]
    public int tailLength;
    [SerializeField]
    private string pelletPrefabName;
    [SerializeField]
    private List<Vector3> pastPositions;
    [SerializeField]
    private List<TailComponent> tailComponents;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawn;
    private GameManager gm;

    void Awake()
    {
        xInput = 0.0f;
        zInput = 0.0f;
        tailLength = 0;
        tailComponents = new List<TailComponent>(10);
        pastPositions = new List<Vector3>(100);
        gm = Camera.main.GetComponent<GameManager>();
    }

    void Update() //if the number of stored positions gets too big, it needs to be culled to prevent performance issues
    {
        if (pastPositions.Count > 201)
        {
            pastPositions.RemoveAt(201);
        }
    }

    // Update is called once per frame
    void FixedUpdate() //rotation and movement handled in FixedUpdate() though not handled by physics
    {
        transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);

        movementDirection = new Vector3 (xInput, 0f, zInput);
        nma.Move(movementDirection * Time.fixedDeltaTime * nma.speed);

        if (movementDirection != Vector3.zero) //if the player's position hasn't changed, don't need to draw the tail or update the position list
        {
            lastPosition = currentPosition;
            currentPosition = this.transform.position;
            if (lastPosition!=currentPosition)
            {
                pastPositions.Insert(0, this.transform.position);
                DrawTail();
            } 
        }
        


    }

    //---Collision Handling
    void OnCollisionEnter(Collision other)
    {
    }

    void OnTriggerEnter(Collider other)
    {

    }

    //---Input Action Events---
    void OnMove(InputValue value) //get the InputValue and store them
    {
        Vector2 inputVector = value.Get<Vector2>();
        xInput = inputVector.x;
        zInput = inputVector.y;
    }

    void OnStun() //when the stun action is executed, check the ability to remove components from tail and if the op is possible, instantiate the stun projectile
    {
        
        if (ValidateComponentRemoval(1))
        {
            DecreaseTail(1);
            Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        }
        else
        {
            Debug.LogError("tail length not long enough");
        }
        
    }

    void OnDash() //when the dash action is executed, check the ability to remove components from tail and if the op is possible, begin the dash powerup
    {
        nma.speed = 10;
        if (ValidateComponentRemoval(2))
        {
            DecreaseTail(2);
        }
        else
        {
            Debug.LogError("tail length not long enough");
        }
        StartCoroutine(DashCoroutine());
    }

    void OnLure() //when the lure action is executed, check if the clicked point is on the navmesh. If so, check the ability to remove components from tail and if the op is possible, begin the lure powerup
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500)) {
            float distanceToHitPoint = Vector3.Distance(this.transform.position, hit.point);
            if (distanceToHitPoint<5f)
            {
                if (ValidateComponentRemoval(2))
                {
                    DecreaseTail(2);
                    gm.SetLure(hit.point);
                }
                else
                {
                    Debug.LogError("tail length not long enough");
                }
            }
            else if (distanceToHitPoint>=5f &&  distanceToHitPoint<10f)
            {
                if (ValidateComponentRemoval(5))
                {
                    DecreaseTail(4);
                    gm.SetLure(hit.point);
                }
                else
                {
                    Debug.LogError("tail length not long enough");
                }
                
            }
            else if (distanceToHitPoint>=10f)
            {
                if (ValidateComponentRemoval(10))
                {
                    DecreaseTail(8);
                    gm.SetLure(hit.point);
                }
                else
                {
                    Debug.LogError("tail length not long enough");
                }
            }
        }
        else
        {
            Debug.LogError("Point clicked was not on the NavMesh");
        }
    }


    //---Tail Handling Functions---
    void DrawTail() //evenly spaces the tail components based on the desired spacing/offset and the current speed (except when ramping up and down)
    {
        int offset = 50;
        foreach (TailComponent tc in tailComponents)
        {
            int indexToRef = (int)(offset / nma.speed);
            Vector3 tailSegmentPosition = pastPositions[indexToRef];
            tc.transform.position = tailSegmentPosition;
            tc.transform.LookAt(tailSegmentPosition);
            offset += 50;
        }  
    }

    void IncreaseTail() //gets the last index of the tail's length and also checks the length - if it's less than 10, more pellets need to be added and if not, then the component at should stack
    {
        int lastTailIndex = tailLength % 10;
        if (tailLength<10)
        {
            GameObject newTailPellet = (GameObject)Instantiate(Resources.Load(pelletPrefabName));
            tailComponents.Insert(lastTailIndex, newTailPellet.GetComponent<TailComponent>());
            tailComponents[lastTailIndex].GetComponentValue();
            
        }
        else
        {
            tailComponents[lastTailIndex].RaiseValue(1);
        }
        tailLength++;
    }

        //when being called for manual reduction for powerups or killing ghosts
    
        public void DecreaseTail(int times)
        {
            

        for (int i = 0; i < times; i++) {

            if (tailLength > 10)
            {
                int lastTailIndex = tailLength % 10;
                tailLength--;
                tailComponents[lastTailIndex-1].LowerValue(1);
                
            }
            else
            {
                TailComponent deadTailPellet = tailComponents[tailLength - 1];
                tailComponents.Remove(deadTailPellet);
                Destroy(deadTailPellet.gameObject);
                //tailComponents.Insert(tailLength-1, new TailComponent t);

                tailLength--;
            }

        }
        }

    public bool ValidateComponentRemoval(int amountToRemove) //checks if the amount of pellets needed to be removed from the tail can be removed without generating a negative value
    {
        if (tailLength>=amountToRemove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //---Skill Handling Coroutines---
    IEnumerator DashCoroutine() //runs the dash coroutine for 5 seconds, then reduces the player's speed
    {
        yield return new WaitForSeconds(5f);
        nma.speed = 5;
    }
}
