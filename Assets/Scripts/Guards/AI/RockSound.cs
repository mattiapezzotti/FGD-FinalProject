using UnityEngine;

public class RockSound : MonoBehaviour
{
    private AudioSource soundSource;
    public float soundRange = 10f;
    private bool hasBeenThrown = false;
    private bool hasPlayed = false;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();     
    }
    public void MarkAsThrown()
    {
        hasBeenThrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenThrown || hasPlayed)
        {
            return;
        }

        hasPlayed = true;
        soundSource.Play();
        EmitSound(true);
    }

    public void EmitSound(bool replayClip = false)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, soundRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Guard"))
            {
                var guardAI = collider.GetComponent<AIController>();

                if (guardAI != null)
                {
                    // Debug.Log($"Guard {guardAI.npcNum} heard sound at position {transform.position} with range {soundRange}");
                    guardAI.HearSound(transform.position, replayClip);
                }
            }
        }
    }
}
