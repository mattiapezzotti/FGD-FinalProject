using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private bool gotToNearestWP; 
    public PatrolState(GameObject npc, Transform player, NavMeshAgent agent, Animator anim, int npcNum, bool goToNearestWP = false)
        : base(npc, player, agent, anim, npcNum)
    {
        currentState = STATE.PATROL;
        // Set the patrol speed for the NPC
        agent.speed = patrolSpeed;
        agent.isStopped = false;
        this.gotToNearestWP = goToNearestWP;
        
    }

    public override void Enter()
    {
        // Debug.Log("Entering PatrolState: " + gotToNearestWP);
        if (gotToNearestWP)
        {
            GameEnviroment.Singleton.SetIndexToNearestWP(npcNum, npc.transform.position);
        }
        agent.ResetPath();
        anim.SetTrigger("IsPatrolling");
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
        else if (agent != null && !agent.hasPath)
        {
            agent.SetDestination(GameEnviroment.Singleton.GetWaypointList(npcNum)[GameEnviroment.Singleton.GetCurrentWaypointIndex(npcNum)].transform.position);
            
            GameEnviroment.Singleton.SetCurrentWaypointIndex(npcNum);
        }
        else if(agent.remainingDistance < 0.1f)
        {
            
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
