using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory { get; private set; }
    public List<ItemData> items = new();
    public InventoryUI inventoryUI;

    void Awake()
    {
        if (inventory != null && inventory != this)
            Destroy(this);
        inventory = this;
    }

    public void AddItem(ItemData itemData)
    {
        items.Add(itemData);
        inventoryUI.UpdateUI();
    }

    public void RemoveItem(string itemID)
    {
        ItemData itemToRemove = FindItem(itemID);
        items.Remove(itemToRemove);
        inventoryUI.UpdateUI();
    }

    public bool IsItemInInventory(string itemToFind)
    {
        foreach (ItemData item in items)
        {
            if (item.id == itemToFind)
                return true;
        }
        return false;
    }
    
        public ItemData FindItem(string itemToFind)
    {
        foreach (ItemData item in items)
        {
            if (item.id == itemToFind)
                return item;
        }
        return null;
    }
}