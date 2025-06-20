using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] stoneFootstepsClips;
    public AudioClip[] woodFootstepsClips;
    public AudioClip[] outsideFootstepsClips;
    private Ray r;
    private RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayFootstep()
    {
        r = new(transform.position, transform.up * -1);

        if (Physics.Raycast(r, out hit, 2f))
        {
            if (hit.transform.gameObject.name.Contains("Balcony") || hit.transform.gameObject.name.Contains("Steps"))
            {
                int randInt = Random.Range(0, woodFootstepsClips.Length);
                audioSource.clip = woodFootstepsClips[randInt];

            }
            else
            {
                if (hit.transform.gameObject.name.Contains("Ground"))
                {
                    int randInt = Random.Range(0, outsideFootstepsClips.Length);
                    audioSource.clip = outsideFootstepsClips[randInt];
                }
                else
                {
                    int randInt = Random.Range(0, stoneFootstepsClips.Length);
                    audioSource.clip = stoneFootstepsClips[randInt];
                }
            }
        audioSource.Play();
        }
    }
}
