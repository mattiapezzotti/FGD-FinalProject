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
    private PlayerSounds playerSounds;
    private TutorialManager tutorialManager;
    private bool seenTutorial = false;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerSounds = GetComponentInChildren<PlayerSounds>();
        tutorialManager = GetComponent<TutorialManager>();
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
                    if (!seenTutorial)
                    {
                        tutorialManager.TriggerAreaStep(5);
                        seenTutorial = true;
                    }
                    if (!interacted.IsOnCooldown())
                    {
                        playerSounds.PlayGuardVoiceline();
                        animator.SetTrigger("Mark");
                        interacted.MarkEnemy();
                    }

                }
            }
        }
    }
    
}
