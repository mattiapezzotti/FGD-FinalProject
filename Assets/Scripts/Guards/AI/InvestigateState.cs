using UnityEngine;
using UnityEngine.AI;


public class InvestigateState : State
{
    private Vector3 soundPosition;
    bool confused = false;
    private float idleDuration = 2f; // Durata in secondi
    private float idleTimer = 0f;

    public InvestigateState(GameObject npc, Transform player, NavMeshAgent agent, Animator anim, int npcNum, Vector3 soundPosition)
        : base(npc, player, agent, anim, npcNum)
    {
        this.soundPosition = soundPosition;
        currentState = STATE.INVESTIGATE;
        agent.speed = investigateSpeed;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        Debug.Log("Entering InvestigateState: " + npc.name);
        
        agent.ResetPath();
        base.Enter();
        confused = true;
        // disattiva l'ExclamationMark se presente tra i figli
        Transform exclamation = npc.transform.Find("ExclamationMark");
        // attiva il QuestionMark
        Transform question = npc.transform.Find("QuestionMark");
        if (exclamation != null)
            exclamation.gameObject.SetActive(false);
        if (question != null)
            question.gameObject.SetActive(true);

        npc.GetComponent<AIController>().PlayConfusedLine();
    }


    public override void Update()
    {

        //ruota lnp vero la posizione del suono
        Vector3 direction = (soundPosition - npc.transform.position).normalized;
        direction.y = 0; // Mantieni la rotazione solo sull'asse Y
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRotation, Time.deltaTime * 2.5f); // 2.5f è la velocità di rotazione, puoi modificarla
        }


        if (confused) 
        {
            idleTimer += Time.deltaTime;
            anim.SetTrigger("IsIdle");
            if (idleTimer >= idleDuration)
            {
                anim.ResetTrigger("IsIdle");
                confused = false; // Reset the confused state after idle duration
            }
        }
        
        if (CanSeePlayer())
        {
            // If the NPC can see the player, switch to the follow state
            nextState = new FollowState(npc, player, agent, anim, npcNum);
            stage = EVENT.EXIT;
        }
        else if (!agent.hasPath && !confused)
        {
            agent.SetDestination(soundPosition);
            anim.SetTrigger("IsPatrolling");
        }
        if (agent.hasPath && agent.remainingDistance < 0.1f)
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

    public void SetInvestigatePosition(Vector3 newPosition)
    {
        soundPosition = newPosition;
        agent.SetDestination(soundPosition);
    }

}
