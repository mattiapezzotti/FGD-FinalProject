using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] guardClips;
    public AudioClip pickUpClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGuardVoiceline()
    {
        int randInt = Random.Range(0, guardClips.Length);
        audioSource.clip = guardClips[randInt];
        audioSource.Play();
    }

    public void PlayPickUp()
    {
        audioSource.clip = pickUpClip;
        audioSource.Play();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }
}
