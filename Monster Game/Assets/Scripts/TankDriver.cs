using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankDriver : MonoBehaviour
{
    [SerializeField]
    Transform player;

    NavMeshAgent navMeshAgent;



    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();


    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) > 15)
        {
            SetDestination();
            navMeshAgent.isStopped = false;
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        

    }

    void SetDestination()
    {
        Vector3 targetVector = player.transform.position;
        navMeshAgent.SetDestination(targetVector);
    }
}
