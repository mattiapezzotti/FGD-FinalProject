using UnityEngine;

public class SwitchInteract : MonoBehaviour, IInteractable
{
    private readonly float rotazioneFinaleX = 35f, velocità = 2f, altezzaGate = 8.0f;

    private Quaternion rotazioneFinaleLeva;
    private Vector3 posizioneFinaleGate;

    private Outline outline;

    public Transform leva;
    public Transform gate;

    private bool attiva = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        rotazioneFinaleLeva = Quaternion.Euler(rotazioneFinaleX, leva.eulerAngles.y, leva.eulerAngles.z);
        posizioneFinaleGate = new Vector3(gate.position.x, altezzaGate, gate.position.z);
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

        leva.rotation = Quaternion.Lerp(leva.rotation, rotazioneFinaleLeva, Time.deltaTime * velocità);
        gate.position = Vector3.Lerp(gate.position, posizioneFinaleGate, Time.deltaTime * velocità);
    }
}
