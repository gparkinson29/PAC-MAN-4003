using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public GameManager info;
    public GameObject player1;
    public Vector3 playerLocation;

    private bool chase, flee, lured; //manages enemy states
    public bool stunned; //manages enemy states
    string nombre; 

    int m_CurrentWaypointIndex = 0;
    int randomnum; //random waypoint index


    // Start is called before the first frame update
    void Start()
    {
        m_CurrentWaypointIndex = 0; 
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        chase = false;
        flee = false;
        lured = false;
        stunned = false;
        info = Camera.main.GetComponent<GameManager>();
        nombre = this.tag;
        player1 = GameObject.FindWithTag("Player");
        //playerInfo = player1.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        behaviorCheck();
        behaviorTree(); 
    }


    //---Behaviors---
    void behaviorCheck() //checks the distance from the player and sets its behavior accordingly
    {
        float dist = Vector3.Distance(this.transform.position, player1.transform.position);
        if(dist < 5 && !lured)
        {
            chase = true;
        }
        else
        {
            chase = false; 
        }
    }
    
    void behaviorTree() //sets the behavior of the enemy based upon its current state
    {
        if (chase) {

            navMeshAgent.SetDestination(player1.transform.position);

        }
        else if (flee) { }
        else if (!lured && !chase)
        {
            patrol();
        }
        else
        {

        }
    }

    void patrol()
    {

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {

            randomnum = Random.Range(0, 4);
            
            navMeshAgent.SetDestination(waypoints[randomnum].position);
            Debug.Log(randomnum);

        }
    }

    public void OnTriggerEnter(Collider cos)
    {
        if (cos.gameObject.tag.Equals("Player"))
        {
            Debug.Log("boop");
            info.checkKill(nombre); 
        }
        if (cos.gameObject.layer==LayerMask.NameToLayer("Enemy")) //if the enemy is colliding with another enemy, force a repath to a new destination so they don't "clump" together
        {
            navMeshAgent.ResetPath();
            patrol();
        }
        if (cos.gameObject.tag=="Stun") //if the enemy is struck by a stun projectile, call the stun behavior and destroy the stun projectile
        {
            Stun();
            Destroy(cos.gameObject);
        }
    }

    //---Responses to powerups---
    void Stun() //because the player and the enemy are both moved with navmesh agents, they will always attempt to avoid one another, so it is necessary to set the radius of the enemy to a tiny amount for the duration of the powerup so that the player can clip through the enemy
    {
        navMeshAgent.radius = 0.1f;
        stunned = true;
        StartCoroutine(StunCoroutine());
    }

    public void Lure(Vector3 positionToMove) //moves enemies to the clicked point on the navmesh
    {
        navMeshAgent.SetDestination(positionToMove);
        lured = true;
        StartCoroutine(LureCoroutine(positionToMove));
    }


    IEnumerator StunCoroutine() //runs stun powerup conditions for 5 seconds, then resets conditions
    {
        yield return new WaitForSeconds(5f);
        navMeshAgent.radius = 1.0f;
        stunned = false;
    }

    IEnumerator LureCoroutine(Vector3 positionToMove) //runs lure powerup conditions until the agent has reached the destination, then resets the conditions
    {
        while (lured)
        {
            if (Vector3.Distance(this.transform.position, positionToMove) < 5f)
            {
                
                yield return new WaitForSeconds(1.5f);
                lured = false;
                navMeshAgent.ResetPath();
            }
            yield return null;
        }
    }
        
        


}
