using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator anim;
    public Transform player;
    private State state;
    public AudioSource audioSource;
    public AudioClip[] confusedLinesClips;
    public AudioClip[] alertLinesClips;
    public int npcNum; // NPC number to differentiate between different NPCs


    public void HearSound(Vector3 soundPosition, bool replayClip = false)
    {
        if (state.currentState != State.STATE.CHASE)
        {
            if (state.currentState == State.STATE.INVESTIGATE && !replayClip)
            {
                if (state is InvestigateState investigateState)
                {
                    investigateState.SetInvestigatePosition(soundPosition);
                }
            }
            else
            {
                state = new InvestigateState(this.gameObject, player, agent, anim, npcNum, soundPosition);
            }
        }
    }

    public void PlayConfusedLine()
    {
        if (audioSource == null)
        {
            return;
        }
        if (confusedLinesClips.Length > 0)
        {
            AudioClip clip = confusedLinesClips[Random.Range(0, confusedLinesClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
    public void PlayAlertLine()
    {
        if (audioSource == null)
        {
            return;
        }
        if (alertLinesClips.Length > 0)
        {
            AudioClip clip = alertLinesClips[Random.Range(0, alertLinesClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        state = new PatrolState(this.gameObject, player, agent, anim, npcNum);
    }

    void Update()
    {
        state = state.Process();
    }

    public State.STATE GetGuardState()
    {
        return state.currentState;
    }

}
