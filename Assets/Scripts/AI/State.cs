using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
  public enum STATE
    {
        IDLE, PATROL, CHASE
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE curentState;
    protected EVENT stage;
    protected GameObject npc;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;
    protected float patrolSpeed;
    protected float chaseSpeed = 4f;

    float visDistance = 10f;
    float visAngle = 45f;
 
    

    public State(GameObject npc, Transform player, NavMeshAgent agent)
    {
        this.npc = npc;
        this.player = player;
        this.agent = agent;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        switch (stage)
        {
            case EVENT.ENTER:
                Enter();
                break;
            case EVENT.UPDATE:
                Update();
                break;
            case EVENT.EXIT:
                Exit();
                return nextState;
        }
        return this;
    }
}
