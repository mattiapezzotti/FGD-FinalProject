using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class FollowState : State
{
    bool confused = false;
    private float idleDuration = 2f; // Durata in secondi
    private float idleTimer = 0f;

    public FollowState(GameObject npc, Transform player, NavMeshAgent agent, Animator anim, int npcNum)
        : base(npc, player, agent, anim, npcNum)
    {
        currentState = STATE.CHASE;
        // Set the chase speed for the NPC
        agent.speed = chaseSpeed;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        Debug.Log("Entering FollowState: " + npc.name);
        anim.SetTrigger("IsChasing");
        base.Enter();
        agent.SetDestination(player.position);
    }

    public override void Update()
    {
        Debug.Log($"IsChasing: {anim.GetBool("IsChasing")} IsIdle: {anim.GetBool("IsIdle")}");

        if (CanSeePlayer())
        {
            // Aggiorna sempre la destinazione verso il player
            agent.isStopped = false;
            agent.SetDestination(player.position);

            idleTimer = 0f;
            confused = false;
            anim.ResetTrigger("IsIdle");
            anim.SetTrigger("IsChasing");

            if (agent.remainingDistance < 0.1f)
            {
                Debug.Log("Reached player: " + player.name);
                // Logica quando raggiunge il player
            }
        }
        else
        {
            if (!confused)
            {
                confused = true;
                agent.ResetPath();
                anim.ResetTrigger("IsChasing");
                anim.SetTrigger("IsIdle");
                idleTimer = 0f;
            }

            if (confused)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleDuration)
                {
                    nextState = new PatrolState(npc, player, agent, anim, npcNum, true);
                    stage = EVENT.EXIT;
                }
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("IsChasing");
        anim.ResetTrigger("IsIdle");
        base.Exit();
    }
}
