using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Roomba : MonoBehaviour
{

    private Vector3 destination;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    private Vector3 GenererDestinationAlea()
    {
        return new Vector3(Random.Range(-2.5f, 12.5f), 0.0f, Random.Range(-2.5f, 12.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending)
        {
            float distanceDestination = Vector3.SqrMagnitude(destination - transform.position);
            if(Mathf.Approximately(agent.velocity.sqrMagnitude, 0.0f) ||
                Mathf.Approximately(distanceDestination, 0.0f) || !agent.hasPath)
            {
                destination = GenererDestinationAlea();
                agent.destination = destination;
            }
        }
    }
}
