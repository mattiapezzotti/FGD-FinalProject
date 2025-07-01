using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI tutorial;
    private bool isTutorialOn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        GameEnviroment.Reset();
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        PlayerPrefs.SetInt("ShowTutorial", 1);
        PlayerPrefs.Save();
    }

    public void SwitchTutorialVisibility()
    {
        if (!isTutorialOn)
        {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.Save();
            tutorial.SetText("Tutorials are ON");
            isTutorialOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);
            PlayerPrefs.Save();
            tutorial.SetText("Tutorials are OFF");
            isTutorialOn = false;
        }


    }
}
