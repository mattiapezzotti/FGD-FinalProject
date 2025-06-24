using UnityEngine;
using UnityEngine.AI;

public class GuardMovement : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public Transform destination;
    private float currentSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = agent.velocity.magnitude;
        animator.SetFloat("velocity", currentSpeed);
        agent.SetDestination(destination.position);
    }
}
