using UnityEngine;
using UnityEngine.AI;


public class InvestigateState : State
{
    private Vector3 soundPosition;
    bool confused = false;
    private float idleDuration = 2f; // Durata in secondi
    private float idleTimer = 0f;

    public InvestigateState(GameObject npc, Transform player, NavMeshAgent agent, Animator anim, int npcNum, Vector3 soundPosition)
        : base(npc, player, agent, anim, npcNum)
    {
        this.soundPosition = soundPosition;
        currentState = STATE.INVESTIGATE;
        agent.speed = investigateSpeed;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        Debug.Log("Entering InvestigateState: " + npc.name);
        
        agent.ResetPath();
        base.Enter();
        confused = true;
        npc.transform.LookAt(soundPosition); // Orient the NPC towards the sound position

    }


    public override void Update()
    {
        if (confused) 
        {
            idleTimer += Time.deltaTime;
            anim.SetTrigger("IsIdle");
            if (idleTimer >= idleDuration)
            {
                anim.ResetTrigger("IsIdle");
                confused = false; // Reset the confused state after idle duration
            }
        }
        
        if (CanSeePlayer())
        {
            // If the NPC can see the player, switch to the follow state
            nextState = new FollowState(npc, player, agent, anim, npcNum);
            stage = EVENT.EXIT;
        }
        else if (!agent.hasPath && !confused)
        {
            agent.SetDestination(soundPosition);
            anim.SetTrigger("IsPatrolling");
        }
        if (agent.hasPath && agent.remainingDistance < 0.1f)
        {
            Debug.Log("Reached sound position: " + soundPosition);
            // Here you can add logic for when the NPC reaches the sound position
            nextState = new IdleState(npc, player, agent, anim, npcNum);
            stage = EVENT.EXIT;
        }

    }
    public override void Exit()
    {
        anim.ResetTrigger("IsPatrolling");
        base.Exit();
    }
}
