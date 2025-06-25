using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform contentPanel; // Il contenitore (es: Content)
    public GameObject itemPrefab;  // Il prefab della cella
    public List<ItemData> allItems; // Lista di oggetti riconoscibili con ID e Sprite

    void Start()
    {
        UpdateUI(); // opzionale all'avvio
    }

    public void UpdateUI()
    {
        // Pulisci prima
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        // Ricrea tutte le celle
        foreach (string id in Inventory.inventory.items)
        {
            var foundItem = allItems.Find(item => item.id == id);
            if (foundItem != null)
            {
                GameObject obj = Instantiate(itemPrefab, contentPanel);
                obj.GetComponent<InventoryItemUI>().SetData(foundItem.icon, foundItem.id);
            }
        }
    }

    public void AddNewItem(string id)
    {
        var foundItem = allItems.Find(item => item.id == id);
        if (foundItem != null)
        {
            GameObject obj = Instantiate(itemPrefab, contentPanel);
            obj.GetComponent<InventoryItemUI>().SetData(foundItem.icon, foundItem.id);
        }
    }
}
