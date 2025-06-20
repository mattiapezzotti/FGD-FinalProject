using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] guardClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGuardVoiceline()
    {
        int randInt = Random.Range(0, guardClips.Length);
        audioSource.clip = guardClips[randInt];
        audioSource.Play();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }
}
