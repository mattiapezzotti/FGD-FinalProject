using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialPanel;
    public bool active;

    void Start()
    {
        tutorialPanel.SetActive(active);
        ShowStep(0);
    }

    public void TriggerAreaStep(int triggeredStep)
    {
        ShowStep(triggeredStep);
    }

    void ShowStep(int i)
    {
        tutorialPanel.SetActive(true);
        switch (i)
        {
            case 0:
                tutorialText.text = "Use the <b>mouse</b> to look around and use <b>WASD</b> to move, hold <b>LEFT SHIFT</b> to run<br>While walking you make noise, running makes the most noise";
                break;
            case 1:
                tutorialText.text = "Press <b>C</b> to crouch, while crouched your footsteps are muffled";
                break;
            case 2:
                tutorialText.text = "Press <b>F</b> to interact with items like these rocks!<br>You can also pickpocket an unsuspicious guard if you are close enough";
                break;
            case 3:
                tutorialText.text = "Press <b>Q</b> to throw rocks and distract guards";
                break;
            case 4:
                tutorialText.text = "Press <b>E</b> while looking at a guard to mark it";
                break;
            default:
                tutorialText.text = "";
                tutorialPanel.SetActive(false);
                break;
        }
    }

}