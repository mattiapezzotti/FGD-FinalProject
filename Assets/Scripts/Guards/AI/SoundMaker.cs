using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    public AudioSource soundSource;
    public float soundRange = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (soundSource.isPlaying)
        {
            return;
        }

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
