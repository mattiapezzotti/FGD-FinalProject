using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public int triggerStep;
    public TutorialManager tutorialManager;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            tutorialManager.TriggerAreaStep(triggerStep);
            activated = true;
        }
    }
}
