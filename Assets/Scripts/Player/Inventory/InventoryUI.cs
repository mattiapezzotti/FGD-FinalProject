using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform contentPanel; // Il contenitore
    public GameObject itemPrefab;  // Il prefab della cella

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Pulisci
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        // Ricrea tutte le celle
        foreach (ItemData item in Inventory.inventory.items)
        {
            GameObject obj = Instantiate(itemPrefab, contentPanel);
            if (item.id == "Rock")
                item.displayName = "Rock (" + Inventory.inventory.GetRockCount() + ")";
            obj.GetComponent<InventoryItemUI>().SetData(item.icon, item.displayName);
        }
    }
}
