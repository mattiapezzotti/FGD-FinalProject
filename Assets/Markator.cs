using UnityEngine;
public interface IMarkable
{
    public void MarkEnemy();
    public bool IsOnCooldown();
}

public class Markator : MonoBehaviour
{
    public Transform source;
    public float range;
    private Ray r;
    private RaycastHit hit;
    private Animator animator;
    private VoiceLines voicelines;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        voicelines = GetComponentInChildren<VoiceLines>();
    }

    // Update is called once per frame
    void Update()
    {
        r = new (source.position, source.forward);

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (Physics.Raycast(r, out hit, range))
            {
                if (hit.collider.gameObject.TryGetComponent(out IMarkable interacted))
                {
                    if (!interacted.IsOnCooldown())
                    {
                        voicelines.PlayGuardVoiceline();
                        animator.SetTrigger("Mark");
                        interacted.MarkEnemy();
                    }

                }
            }
        }
    }
    
}
