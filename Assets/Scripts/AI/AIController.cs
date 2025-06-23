using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    NavMeshAgent agent;
    //Animator anim; // Uncomment if you have an Animator component
    public Transform player; // Reference to the player Transform
    State currentState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>(); // Uncomment if you have an Animator component
        currentState = new IdleState(this.gameObject, player, agent);//da aggiungere anim
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
        
    }
}
