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
    public TutorialManager tutorialManager;
    private bool seenTutorial = false;

    public GameObject rockPrefab;
    public float throwForce = 10f;

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
                    if (!seenTutorial)
                    {
                        tutorialManager.TriggerAreaStep(3);
                        seenTutorial = true;
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
    }
}