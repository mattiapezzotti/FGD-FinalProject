using System;
using UnityEngine;

public class KeyInteract : MonoBehaviour, IInteractable
{
    public string keyID;

    private Outline outline;
    public PlayerSounds audioSource;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void DrawOutline(bool b)
    {
        outline.enabled = b;
    }

    public void Interact()
    {
        audioSource.PlayPickUp();
        gameObject.SetActive(false);
        Inventory.inventory.AddItem(keyID);
    }
}