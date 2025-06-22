using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    private float idleDuration = 3f; // Durata in secondi
    private float idleTimer = 0f;

    public IdleState(GameObject npc, Transform player, NavMeshAgent agent)
        : base(npc, player, agent)
    {
        curentState = STATE.IDLE;
    }

    public override void Enter()
    {
        //anim trrigger 

        idleTimer = 0f; // Reset del timer ogni volta che si entra nello stato
        base.Enter();
    }

    public override void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer < idleDuration)
        {
            // Rimani in idle, non fare nulla
            return;
        }

        // Dopo idleDuration secondi, puoi aggiungere qui la logica per cambiare stato
        nextState = new PatrolState(npc, player, agent);
        stage = EVENT.EXIT;

        base.Update();
    }

    public override void Exit()
    {
        //anim reset trigger


        base.Exit();
    }
}
