using UnityEngine;

public class GuardInventory : MonoBehaviour, IPickpocketer
{
    public ItemData guardItemId;
    private bool hasItem;
    private Outline outline;
    private bool canBePickpocketed;
    private bool hasBeenPickpocketed = false;

    void Start()
    {
        hasItem = guardItemId.id != "null";
        outline = GetComponent<Outline>();
    }

    public void Pickpocket()
    {
        hasBeenPickpocketed = true;
        if (hasItem)
        {
            Inventory.inventory.AddItem(guardItemId);
        }

        hasItem = false;
    }

    public void DrawOutline(bool b)
    {
        outline.enabled = b;
    }

    public bool CanBePickpocketed()
    {
        return canBePickpocketed;
    }

    void Update()
    {
        canBePickpocketed = !hasBeenPickpocketed && GetComponent<AIController>().GetGuardState() != State.STATE.CHASE;
    }
}
