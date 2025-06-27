using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    private bool goToNearestWP;
    private float idleDuration = 3f; // Durata in secondi
    private float idleTimer = 0f;

    public IdleState(GameObject npc, Transform player, NavMeshAgent agent, Animator anim, int npcNum, bool goToNearestWP = false)
        : base(npc, player, agent, anim, npcNum)
    {
        currentState = STATE.IDLE;
        this.goToNearestWP = goToNearestWP;
    }

    public override void Enter()
    {
        Debug.Log("Entering IdleState: " + npc.name);
        anim.SetTrigger("IsIdle");
        idleTimer = 0f; // Reset del timer ogni volta che si entra nello stato
        base.Enter();
    }

    public override void Update()
    {
        idleTimer += Time.deltaTime;

        if (CanSeePlayer())
        {
            // If the NPC can see the player, switch to the follow state
            nextState = new FollowState(npc, player, agent, anim, npcNum);
            stage = EVENT.EXIT;
        }
        else if (idleTimer < idleDuration)
        {
            // Rimani in idle, non fare nulla
            return;
        }
        

        // Dopo idleDuration secondi, puoi aggiungere qui la logica per cambiare stato
        nextState = new PatrolState(npc, player, agent, anim, npcNum, goToNearestWP);
        stage = EVENT.EXIT;

        
    }

    public override void Exit()
    {
        anim.ResetTrigger("IsIdle");

        base.Exit();
    }
}
