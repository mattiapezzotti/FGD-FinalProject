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
        currentWaypointIndex = GameEnviroment.Singleton.CurrentWaypointIndex;
        //set an animation trigger for patrolling
        Debug.Log("Entering Patrol State, starting at waypoint: " + GameEnviroment.Singleton.WayPoints[currentWaypointIndex].name);
        base.Enter();
    }

    public override void Update()
    {
        if (!agent.hasPath)
        {
            Debug.Log("entrato nel if " + currentWaypointIndex + " " + GameEnviroment.Singleton.CurrentWaypointIndex);
            agent.SetDestination(GameEnviroment.Singleton.WayPoints[currentWaypointIndex].transform.position);


            if (currentWaypointIndex >= GameEnviroment.Singleton.WayPoints.Count - 1)
                GameEnviroment.Singleton.CurrentWaypointIndex = 0;
            else
                GameEnviroment.Singleton.CurrentWaypointIndex = currentWaypointIndex + 1;
        }
        if(agent.remainingDistance < 0.1f)
        {
            Debug.Log("Reached waypoint: " + GameEnviroment.Singleton.WayPoints[currentWaypointIndex].name);
            nextState = new IdleState(npc, player, agent);
            stage = EVENT.EXIT;
        }



    }

    public override void Exit()
    {
        // Reset the animation trigger for patrolling
        //anim reset trigger
        base.Exit();
    }
}
