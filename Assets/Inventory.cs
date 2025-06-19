using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory { get; private set; }
    public List<string> items = new();

    private void Awake()
    {
        if (inventory != null && inventory != this)
            Destroy(this);
        inventory = this;
    }

    public void AddItem(string itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(string itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    public bool FindItem(string itemToFind)
    {
        bool exists = items.Any(s => s.Contains(itemToFind));
        return exists;
    }
}