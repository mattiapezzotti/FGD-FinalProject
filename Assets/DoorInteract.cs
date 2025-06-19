using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    private readonly float yPortaFinale = 190.0f, velocità = 2f;

    private Quaternion rotazioneFinalePorta;
    private readonly string keyID = "BaseStairsKey";
    private Outline outline;

    private bool attiva = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        rotazioneFinalePorta = Quaternion.Euler(transform.eulerAngles.x, yPortaFinale, transform.eulerAngles.z);
    }

    public void Interact()
    {
        if (Inventory.inventory.FindItem(keyID))
        {
            attiva = true;
            outline.OutlineWidth = 0;
        }
        else
        {
            outline.OutlineColor = Color.red;
        }

    }

    public void DrawOutline(bool b)
    {
        outline.enabled = b;
        if (!b)
        {
            outline.OutlineColor = Color.white;
        }
    }

    void Update()
    {
        if (!attiva) return;

        transform.rotation = Quaternion.Lerp(transform.rotation, rotazioneFinalePorta, Time.deltaTime * velocità);
    }
}
