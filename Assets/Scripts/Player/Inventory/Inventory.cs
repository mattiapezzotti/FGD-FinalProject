using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory { get; private set; }
    public List<ItemData> items = new();
    public InventoryUI inventoryUI;

    private int counterRock = 0;

    void Awake()
    {
        if (inventory != null && inventory != this)
            Destroy(this);
        inventory = this;
    }

    public void AddItem(ItemData itemData)
    {
        if (itemData.id == "Rock")
        {
            counterRock++;
            if (!HasItem("Rock"))
            {
                items.Add(itemData);
            }
        }
        else
        {
            items.Add(itemData);
        }

        inventoryUI.UpdateUI();
    }

    public void RemoveItem(string itemID)
    {
        if (itemID == "Rock")
        {
            counterRock--;
            if (counterRock <= 0)
            {
                ItemData rockItem = FindItem("Rock");
                if (rockItem != null)
                {
                    items.Remove(rockItem);
                }
            }
        }
        else
        {
            ItemData itemToRemove = FindItem(itemID);
            if (itemToRemove != null)
                items.Remove(itemToRemove);
        }

        inventoryUI.UpdateUI();
    }

    public bool HasItem(string itemToFind)
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

    public int GetRockCount()
    {
        return counterRock;
    }
}