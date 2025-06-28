using UnityEngine;
using UnityEngine.AI;

public class FollowState : State
{
    bool confused = false;
    private float idleDuration = 1f; // Durata in secondi
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
        // Debug.Log("Entering FollowState: " + npc.name);
        anim.SetTrigger("IsChasing");
        base.Enter();
        agent.SetDestination(player.position);

        // Attiva l'ExclamationMark se presente tra i figli
        Transform exclamation = npc.transform.Find("ExclamationMark");
        // Se il QuestionMark � attivo lo disattiva
        Transform question = npc.transform.Find("QuestionMark");
        if (exclamation != null)
            exclamation.gameObject.SetActive(true);
        if (question != null)
            question.gameObject.SetActive(false);

        npc.GetComponent<AIController>().PlayAlertLine();
    }

    public override void Update()
    {

        if (CanSeePlayer())
        {
            //ruota lnpc    
            Vector3 direction = (player.position - npc.transform.position).normalized;
            direction.y = 0; // Mantieni la rotazione solo sull'asse Y
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRotation, Time.deltaTime * 100f);
            }
            // Aggiorna sempre la destinazione verso il player
            agent.isStopped = false;
            agent.SetDestination(player.position);

            idleTimer = 0f;
            confused = false;
            anim.ResetTrigger("IsIdle");
            anim.SetTrigger("IsChasing");
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
                    nextState = new InvestigateState(npc, player, agent, anim, npcNum, player.transform.position);
                    stage = EVENT.EXIT; // Passa allo stato di InvestigateState
                }
            }
        }
    }

    public override void Exit()
    {
        // Disattiva l'ExclamationMark se presente tra i figli
        Transform exclamation = npc.transform.Find("ExclamationMark");
        if (exclamation != null)
            exclamation.gameObject.SetActive(false);

        anim.ResetTrigger("IsIdle");
        base.Exit();
    }
}
