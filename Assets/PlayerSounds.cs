using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource footstepsAudioSource;
    public AudioClip[] footstepsClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayFootstep() {
        int randInt = Random.Range(0, footstepsClips.Length);
        footstepsAudioSource.clip = footstepsClips[randInt];
        footstepsAudioSource.Play();
    }

    void Start()
    {
        footstepsAudioSource.volume = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
