using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim; 
    public Transform player; // Reference to the player Transform
    State state;
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
                
                // If already investigating, just update the sound position
                InvestigateState investigateState = state as InvestigateState;
                if (investigateState != null)
                {
                    investigateState.SetInvestigatePosition(soundPosition);
                }
            }
            else
            {
                // Transition to InvestigateState when the NPC hears a sound
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
            audioSource.PlayOneShot(clip); // Play the clip once
            //audioSource.Play();
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
            audioSource.PlayOneShot(clip); // Play the clip once
            //audioSource.Play();
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
