using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    int currentWaypointIndex = -1;
    public PatrolState(GameObject npc, Transform player, NavMeshAgent agent)
        : base(npc, player, agent)
    {
        curentState = STATE.PATROL;
        // Set the patrol speed for the NPC
        agent.speed = patrolSpeed;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        currentWaypointIndex = 0;
        //set an animation trigger for patrolling

        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance > 0)
        {
            if (currentWaypointIndex >= GameEnviroment.Singleton.WayPoints.Count - 1)
                currentWaypointIndex = 0;
            else
                currentWaypointIndex++;
            agent.SetDestination(GameEnviroment.Singleton.WayPoints[currentWaypointIndex].transform.position);
        }
        base.Update();
    }

    public override void Exit()
    {
        // Reset the animation trigger for patrolling
        //anim reset trigger
        base.Exit();
    }
}
