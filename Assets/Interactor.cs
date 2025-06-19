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
    public Ray r;
    public RaycastHit hit;
    public IInteractable lastHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        r = new (source.position, source.forward);

        if (Physics.Raycast(r, out hit, range))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interacted))
            {
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

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (Physics.Raycast(r, out hit, range))
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interacted))
                {
                    interacted.Interact();
                }
            }
        }
    }
}
