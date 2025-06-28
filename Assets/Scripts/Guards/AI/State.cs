using UnityEngine;
using UnityEngine.AI;

public class State
{
  public enum STATE
    {
        IDLE, PATROL, CHASE, INVESTIGATE
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE currentState;
    protected EVENT stage;
    protected GameObject npc;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected int npcNum; // NPC number to differentiate between different NPCs
    protected float patrolSpeed = 2.5f;
    protected float chaseSpeed = 3.5f;
    protected float investigateSpeed = 2.0f;

    float visDistance = 7f;
    float visAngle = 30f;
 
    

    public State(GameObject npc, Transform player, NavMeshAgent agent, Animator anim, int npcNum)
    {
        this.npc = npc;
        this.player = player;
        this.agent = agent;
        this.anim = anim;
        this.npcNum = npcNum;
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

    public bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position + Vector3.up * 0.9f) - (npc.transform.position + Vector3.up * 1.5f);
        float angleToPlayer = Vector3.Angle(npc.transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude <= visDistance && angleToPlayer <= visAngle)
        {
            Ray ray = new Ray(npc.transform.position + Vector3.up * 1.5f, directionToPlayer.normalized); // altezza occhi
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (Physics.Raycast(ray, out hit, directionToPlayer.magnitude))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //Debug.Log($"Guard {npcNum} can see player at position {player.position}");
                    return true;
                }
                else
                {
                    //Debug.Log($"Guard {npcNum} cannot see player: obstacle '{hit.collider.name}' in the way");
                }
            }
        }
        return false;
    }

}
