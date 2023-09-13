using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentWaypointIndex = 0; 
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        patrol(); 
    }


    void patrol()
    {

        

        

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {

            int randomnum = Random.Range(0, 4);
            //m_CurrentWaypointIndex = m_CurrentWaypointIndex + 1;
            

            //if (m_CurrentWaypointIndex > 5)
           // {
           //     m_CurrentWaypointIndex = 0;
           // }

            navMeshAgent.SetDestination(waypoints[randomnum].position);
            Debug.Log(randomnum);


        }
    }

}
