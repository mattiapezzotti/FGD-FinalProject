using UnityEngine;

public class GuardInventory : MonoBehaviour, IPickpocketer
{
    public string guardItemId;
    private bool hasItem;
    public InventoryUI inventoryUI;

    void Start()
    {
        hasItem = true;
    }

    public void Pickpocket()
    {
        if (hasItem)
        {
            Inventory.inventory.AddItem(guardItemId);
            inventoryUI.AddNewItem(guardItemId); 
        }
        
        hasItem = false;
    }
}
