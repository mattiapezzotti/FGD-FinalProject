using UnityEngine;

public class ButtonInteract : MonoBehaviour, IInteractable
{
    private readonly float yPortaFinale = 10.0f, velocità = 2f, zBottoneFinale = -0.06f;

    private Quaternion rotazioneFinalePorta;
    private Vector3 posizioneFinaleButton;

    private Outline outline;

    public Transform bottone;
    public Transform porta;

    private bool attiva = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        rotazioneFinalePorta = Quaternion.Euler(porta.eulerAngles.x, yPortaFinale, porta.eulerAngles.z);
        posizioneFinaleButton = new Vector3(bottone.position.x, bottone.position.y, zBottoneFinale);
    }

    public void Interact()
    {
        attiva = true;
        outline.OutlineWidth = 0;
    }

    public void DrawOutline(bool b)
    {
        outline.enabled = b;
    }

    void Update()
    {
        if (!attiva) return;

        porta.rotation = Quaternion.Lerp(porta.rotation, rotazioneFinalePorta, Time.deltaTime * velocità);
        bottone.position = Vector3.Lerp(bottone.position, posizioneFinaleButton, Time.deltaTime * velocità);
    }
}
