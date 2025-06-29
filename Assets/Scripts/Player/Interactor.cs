using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public void DrawOutline(bool b);
}

public class Interactor : MonoBehaviour
{
    public Transform source;
    public float range;
    private Ray r;
    private RaycastHit hit;
    private IInteractable lastHit;
    private TutorialManager tutorialManager;
    private bool seenPickUpTutorial = false;
    private bool seenRockTutorial = false;

    public GameObject rockPrefab;
    public float throwForce = 20f;

    void Start()
    {
        tutorialManager = GetComponent<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        r = new(source.position, source.forward);

        if (Physics.Raycast(r, out hit, range))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interacted))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (!seenPickUpTutorial)
                    {
                        tutorialManager.TriggerAreaStep(3);
                        seenPickUpTutorial = true;
                    }

                    interacted.Interact();
                }

                if (lastHit != null && lastHit != interacted)
                {
                    lastHit.DrawOutline(false);
                }

                interacted.DrawOutline(true);
                lastHit = interacted;
            }
            else
            {
                if (lastHit != null)
                {
                    lastHit.DrawOutline(false);
                    lastHit = null;
                }
            }
        }
        else
        {
            if (lastHit != null)
            {
                lastHit.DrawOutline(false);
                lastHit = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!seenRockTutorial)
            {
                tutorialManager.TriggerAreaStep(4);
                seenRockTutorial = true;
            }
            
            ThrowRock();
        }
    }

    void ThrowRock()
    {
        if (Inventory.inventory.HasItem("Rock") && Inventory.inventory.GetRockCount() > 0)
        {
            Inventory.inventory.RemoveItem("Rock");

            GameObject thrownRock = Instantiate(rockPrefab, source.position + source.forward * 1f, Quaternion.identity);

            if (thrownRock.TryGetComponent<RockSound>(out var soundMaker))
            {
                soundMaker.MarkAsThrown();
            }

            if (thrownRock.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
                Vector3 throwDir = (source.forward + source.up * 0.2f).normalized;
                rb.AddForce(throwDir * throwForce, ForceMode.Impulse);
            }
        }
    }
}