using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
