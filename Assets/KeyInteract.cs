using System;
using UnityEngine;

public class KeyInteract : MonoBehaviour, IInteractable
{
    public string keyID;

    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        Inventory.inventory.AddItem(keyID);
    }

    public void DrawOutline(bool b)
    {
        outline.enabled = b;
    }
}