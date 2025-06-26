using UnityEngine;

public class GuardInventory : MonoBehaviour, IPickpocketer
{
    public ItemData guardItemId;
    private bool hasItem;

    void Start()
    {
        hasItem = true;
    }

    public void Pickpocket()
    {
        if (hasItem)
        {
            Inventory.inventory.AddItem(guardItemId);
        }
        
        hasItem = false;
    }
}
