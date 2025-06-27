using System;
using UnityEngine;

public class RockInteract : MonoBehaviour, IInteractable
{
    public ItemData rockData;

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
        Inventory.inventory.AddItem(rockData);
    }
}