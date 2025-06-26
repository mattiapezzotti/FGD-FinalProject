using UnityEngine;

public class GuardAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] metalFootstepsClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayMetalFootstep()
    {
        // if (audioSource.isPlaying) return;
        int randInt = Random.Range(0, metalFootstepsClips.Length);
        audioSource.clip = metalFootstepsClips[randInt];
        audioSource.Play();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
