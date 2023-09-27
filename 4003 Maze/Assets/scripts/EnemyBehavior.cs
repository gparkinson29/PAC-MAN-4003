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

    private bool chase, flee, lured;
    public bool stunned;
    string nombre; 

    int m_CurrentWaypointIndex = 0;
    int randomnum;


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
    void behaviorCheck()
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
    
    void behaviorTree()
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
        if (cos.gameObject.layer==LayerMask.NameToLayer("Enemy"))
        {
            navMeshAgent.ResetPath();
            patrol();
        }
        if (cos.gameObject.tag=="Stun")
        {
            Stun();
            Debug.Log("collided with enemy");
            Destroy(cos.gameObject);
        }
    }

    //---Responses to powerups---
    void Stun()
    {
        navMeshAgent.radius = 0.1f;
        stunned = true;
        StartCoroutine(StunCoroutine());
    }

    public void Lure(Vector3 positionToMove)
    {
        navMeshAgent.SetDestination(positionToMove);
        lured = true;
        StartCoroutine(LureCoroutine(positionToMove));
    }


    IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(5f);
        navMeshAgent.radius = 1.0f;
        stunned = false;
    }

    IEnumerator LureCoroutine(Vector3 positionToMove)
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
