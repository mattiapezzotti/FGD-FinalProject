using UnityEngine;

public class Guard : MonoBehaviour, IPickpocketer
{
    public string guardItemId;
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
