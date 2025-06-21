using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    private readonly float velocità = 2f;
    public float yPortaFinale;

    private Quaternion rotazioneFinalePorta;
    public string keyID;
    private Outline outline;

    private bool attiva = false;

    private AudioSource audioSource;
    public AudioClip doorOpensClip;
    public AudioClip closedDoorClip;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
        rotazioneFinalePorta = Quaternion.Euler(transform.eulerAngles.x, yPortaFinale, transform.eulerAngles.z);
    }

    public void Interact()
    {
        if (attiva) return;
        if (Inventory.inventory.FindItem(keyID))
        {
            audioSource.clip = doorOpensClip;
            attiva = true;
            outline.OutlineWidth = 0;
            Inventory.inventory.RemoveItem(keyID);
        }
        else
        {
            audioSource.clip = closedDoorClip;
            outline.OutlineColor = Color.red;
        }
        audioSource.Play();
        audioSource.time = 0;
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
