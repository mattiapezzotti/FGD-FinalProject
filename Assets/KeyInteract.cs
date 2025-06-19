using System;
using UnityEngine;

public class KeyInteract : MonoBehaviour, IInteractable
{
    private readonly string id = "BaseStairsKey";
    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        Inventory.inventory.AddItem(id);
    }

    public void DrawOutline(bool b)
    {
        outline.enabled = b;
    }
}