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
                    StartCoroutine(NextStepWithDelay(2f));
                break;
            case 1:
                StartCoroutine(NextStepWithDelay(10f));
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.C))
                    StartCoroutine(NextStepWithDelay(7f));
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.E))
                    StartCoroutine(NextStepWithDelay(2f));
                break;
            case 4:
                StartCoroutine(NextStepWithDelay(5f));
                break;
        }
    }

    void ShowStep(int i)
    {
        tutorialPanel.SetActive(true);
        switch (i)
        {
            case 0:
                tutorialText.text = "Use <b>WASD</b> to move, hold <b>LEFT SHIFT</b> to run";
                break;
            case 1:
                tutorialText.text = "While walking you make noise, running makes the most noise";
                break;
            case 2:
                tutorialText.text = "Press <b>C</b> to crouch, while crouched your footsteps are muffled";
                break;
            case 3:
                tutorialText.text = "You can mark a guard by looking at it and pressing <b>E</b>";
                break;
            case 4:
                tutorialText.text = "Press <b>F</b> to interact<br> Stay close to an unsuspicious guard and press <b>F</b> to pickpocket";
                break;
            default:
                tutorialText.text = "";
                tutorialPanel.SetActive(false);
                break;
        }
    }

}