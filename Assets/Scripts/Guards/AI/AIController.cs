using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator anim; 
    public Transform player; // Reference to the player Transform
    private State state;
    public AudioSource audioSource;
    public AudioClip[] confusedLinesClips;
    public AudioClip[] alertLinesClips; // Lines to play when the NPC is alerted
    public int npcNum; // NPC number to differentiate between different NPCs


    public void HearSound(Vector3 soundPosition, bool replayClip = false)
    {
        if (state.currentState != State.STATE.CHASE)
        {
            if(state.currentState == State.STATE.INVESTIGATE && !replayClip)
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 
        state = new PatrolState(this.gameObject, player, agent, anim, npcNum);//da aggiungere anim
    }

    // Update is called once per frame
    void Update()
    {
        state = state.Process();
    }
}
