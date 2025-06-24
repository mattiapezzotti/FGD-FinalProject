using UnityEngine;

public class TestSoundMaker : MonoBehaviour
{
    public AudioSource soundSource;
    public float soundRange = 10f;

    private void OnMouseDown()
    {
        if (soundSource.isPlaying)
        {
            return;
        }

        soundSource.Play();

        EmitSound();


        //soundSource.MakeSound(sound);
    }

    void EmitSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, soundRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Guard"))
            {
                var guardAI = collider.GetComponent<AIController>();

                if (guardAI != null)
                {
                    Debug.Log($"Guard {guardAI.npcNum} heard sound at position {transform.position} with range {soundRange}");
                }
            }
        }
    }
}
