using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public int triggerStep;
    public TutorialManager tutorialManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.TriggerAreaStep(triggerStep);
        }
    }
}
