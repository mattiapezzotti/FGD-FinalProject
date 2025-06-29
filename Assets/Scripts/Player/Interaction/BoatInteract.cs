using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatInteract : MonoBehaviour, IInteractable
{
    private Outline outline;
    private 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact()
    {
        PauseMenu.pauseMenu.YouWon();
    }

    public void DrawOutline(bool b)
    {
        outline.enabled = b;
    }
}
