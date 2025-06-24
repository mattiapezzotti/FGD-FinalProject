using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class FollowState : State
{
    public FollowState(GameObject npc, Transform player, NavMeshAgent agent, Animator anim, int npcNum)
        : base(npc, player, agent, anim, npcNum)
    {
        curentState = STATE.CHASE;
        // Set the chase speed for the NPC
        agent.speed = chaseSpeed;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        Debug.Log("Entering FollowState: " + npc.name);
        anim.SetTrigger("IsChasing");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        if (agent.hasPath)
        {
            if (agent.remainingDistance < 0.1f)
            {
                Debug.Log("Reached player: " + player.name);

                // Here you can add logic for when the NPC reaches the player
            }
            else if (!CanSeePlayer())
            {
                nextState = new PatrolState(npc, player, agent, anim, npcNum, true);
                stage = EVENT.EXIT;
            }
        }
        //else??
    }

    public override void Exit()
    {
        anim.ResetTrigger("IsChasing");
        base.Exit();
    }
}
