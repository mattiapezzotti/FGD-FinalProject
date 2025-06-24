using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialPanel;
    private int step = 0;
    private bool waiting = false;

    void Start()
    {
        ShowStep(step);
    }

    public void TriggerAreaStep(int triggeredStep)
    {
        if (step == triggeredStep)
        {
            ShowStep(step);
        }
    }

    IEnumerator NextStepWithDelay(float delay)
    {
        waiting = true;
        yield return new WaitForSeconds(delay);
        step++;
        ShowStep(step);
        waiting = false;
    }

    void Update()
    {
        if (waiting) return;

        switch (step)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                    StartCoroutine(NextStepWithDelay(1f));
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.C))
                    StartCoroutine(NextStepWithDelay(2f));
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                    StartCoroutine(NextStepWithDelay(2f));
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.E))
                    StartCoroutine(NextStepWithDelay(2f));
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.F))
                    StartCoroutine(NextStepWithDelay(2f));
                break;
        }
    }

    void ShowStep(int i)
    {
        tutorialPanel.SetActive(true);
        switch (i)
        {
            case 0:
                tutorialText.text = "Use <b>WASD</b> to move";
                break;
            case 1:
                tutorialText.text = "Press <b>C</b> to crouch";
                break;
            case 2:
                tutorialText.text = "Hold <b>Shift</b> to run";
                break;
            case 3:
                tutorialText.text = "Press <b>E</b> to mark enemies";
                break;
            case 4:
                tutorialText.text = "Press <b>F</b> to interact with items";
                break;
            default:
                tutorialText.text = "";
                tutorialPanel.SetActive(false);
                break;
        }
    }

}