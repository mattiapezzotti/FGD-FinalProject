using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim; 
    public Transform player; // Reference to the player Transform
    State state;
    public int npcNum; // NPC number to differentiate between different NPCs


    public void HearSound(Vector3 soundPosition)
    {
        if (state.currentState != State.STATE.CHASE)
        {
            // Transition to InvestigateState when the NPC hears a sound
            state = new InvestigateState(this.gameObject, player, agent, anim, npcNum, soundPosition);
            AlertIcon alert = GetComponentInChildren<AlertIcon>(true); // include oggetti disattivati
            if (alert != null)
            {
                alert.Show();
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 
        state = new PatrolState(this.gameObject, player, agent, anim, npcNum);//da aggiungere anim
    }

    // Update is called once per frame
    void Update()
    {
        state = state.Process();
        
    }
}
