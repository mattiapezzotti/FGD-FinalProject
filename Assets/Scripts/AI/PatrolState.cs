using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private bool fromChase; 
    public PatrolState(GameObject npc, Transform player, NavMeshAgent agent, bool fromChase = false)
        : base(npc, player, agent)
    {
        curentState = STATE.PATROL;
        // Set the patrol speed for the NPC
        agent.speed = patrolSpeed;
        agent.isStopped = false;
        this.fromChase = fromChase;
        // this.npcNum = npcNum;
        // currentWaypointIndex = GameEnviroment.Singleton.getWaypointIndex(npcNum);
    }

    public override void Enter()
    {
        Debug.Log("Entering PatrolState: " + fromChase);
        if (fromChase)
        {
            float minDistance = Vector3.Distance(npc.transform.position, GameEnviroment.Singleton.WayPoints[0].transform.position);
            int closestIndex = 0;

            for(int i = 1; i < GameEnviroment.Singleton.WayPoints.Count; i++)
            {
                float distance = Vector3.Distance(npc.transform.position, GameEnviroment.Singleton.WayPoints[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }
            GameEnviroment.Singleton.CurrentWaypointIndex = closestIndex;

        }
        //set an animation trigger for patrolling
        base.Enter();
    }

    public override void Update()
    {

        if(CanSeePlayer())
        {
            // If the NPC can see the player, switch to the follow state
            nextState = new FollowState(npc, player, agent);
            stage = EVENT.EXIT;
        }
        else if (!agent.hasPath)
        {
            agent.SetDestination(GameEnviroment.Singleton.WayPoints[GameEnviroment.Singleton.CurrentWaypointIndex].transform.position);


            if (GameEnviroment.Singleton.CurrentWaypointIndex >= GameEnviroment.Singleton.WayPoints.Count - 1)
                GameEnviroment.Singleton.CurrentWaypointIndex = 0;
            else
                GameEnviroment.Singleton.CurrentWaypointIndex++;
        }
        else if(agent.remainingDistance < 0.1f)
        {
            //GameEnviroment.Singleton.setWaypointIndex(npcNum, currentWaypointIndex);
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
