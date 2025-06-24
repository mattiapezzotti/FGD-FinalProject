using UnityEngine;
using UnityEngine.AI;


public class InvestigateState : State
{
    private Vector3 soundPosition;

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
        anim.SetTrigger("IsInvestigating");
        agent.ResetPath();
        base.Enter();
        
    }


    public override void Update()
    {
        if (CanSeePlayer())
        {
            // If the NPC can see the player, switch to the follow state
            nextState = new FollowState(npc, player, agent, anim, npcNum);
            stage = EVENT.EXIT;
        }
        else if (!agent.hasPath)
        {
            agent.SetDestination(soundPosition);
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
        anim.ResetTrigger("IsInvestigating");
        base.Exit();
    }
}
