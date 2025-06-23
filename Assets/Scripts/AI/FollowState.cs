using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class FollowState : State
{
    public FollowState(GameObject npc, Transform player, NavMeshAgent agent)
        : base(npc, player, agent)
    {
        curentState = STATE.CHASE;
        // Set the chase speed for the NPC
        agent.speed = chaseSpeed;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        Debug.Log("Entering FollowState: " + npc.name);
        // Set an animation trigger for chasing
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
                nextState = new PatrolState(npc, player, agent, true);
                stage = EVENT.EXIT;
            }
        }
        //else??
    }

    public override void Exit()
    {
        // Reset the animation trigger for chasing
        base.Exit();
    }
}
