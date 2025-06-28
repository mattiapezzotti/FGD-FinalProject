using TMPro;
using UnityEngine;

public interface IPickpocketer
{
    public void Pickpocket();
    public void DrawOutline(bool b);
    bool CanBePickpocketed();
}

public class Pickpocketer : MonoBehaviour
{
    public Transform source;
    public float range;
    private Ray r;
    private RaycastHit hit;
    private Animator animator;
    public TextMeshProUGUI pickpocketText;
    private PlayerSounds playerSounds;

    void Start()
    {
        playerSounds = GetComponentInChildren<PlayerSounds>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        r = new(source.position, source.forward);

        if (Physics.Raycast(r, out hit, range))
        {
            if (hit.collider.gameObject.TryGetComponent(out IPickpocketer interacted))
            {
                if (interacted.CanBePickpocketed())
                {
                    pickpocketText.enabled = true;
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        playerSounds.PlayPickUp();
                        animator.SetTrigger("Pickpocket");
                        interacted.Pickpocket();
                        pickpocketText.enabled = false;
                    }
                }
            }
        }
        else
        {
            pickpocketText.enabled = false;
        }
    }
}
