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
    private int tailLength, spacing;
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

    void Awake()
    {
        xInput = 0.0f;
        zInput = 0.0f;
        tailLength = 0;
        spacing = 10;
        tailComponents = new List<TailComponent>(10);
        pastPositions = new List<Vector3>(100);
    }

    void Update()
    {

        if (pastPositions.Count > 201)
        {
            pastPositions.RemoveAt(201);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementDirection = new Vector3 (xInput, 0f, zInput);
        nma.Move(movementDirection * Time.deltaTime * nma.speed);
        
        transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);

        if (movementDirection != Vector3.zero)
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
    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        xInput = inputVector.x;
        zInput = inputVector.y;
    }

    void OnStun()
    {
        Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
    }

    void OnDash()
    {
    }

    void OnLure()
    {

    }


    //---Tail Handling Functions---
    void DrawTail()
    {
        int index = 1;
        foreach (TailComponent tc in tailComponents)
        {
            Vector3 tailSegmentPosition = pastPositions[index * spacing];
            tc.transform.position = tailSegmentPosition; 
            tc.transform.LookAt(tailSegmentPosition);
            index++;
        }
    }

    void IncreaseTail()
    {
        int lastTailIndex = tailLength % 10;
        if (tailLength<10)
        {
            GameObject newTailPellet = (GameObject)Instantiate(Resources.Load(pelletPrefabName));
            tailComponents.Insert(lastTailIndex, newTailPellet.GetComponent<TailComponent>());
            
        }
        else
        {
            tailComponents[lastTailIndex].RaiseValue(1);
        }
        tailLength++;
    }

    void DecreaseTail()
    {
        int lastTailIndex = tailLength % 10;
        if (tailLength>10)
        {
            tailComponents[lastTailIndex].LowerValue(1);
        }
        else
        {
        }
    }

    //---Skill Handling Coroutines---
    IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(5f);
    }
}
