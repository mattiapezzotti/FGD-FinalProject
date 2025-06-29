using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour

{
    public static PauseMenu pauseMenu { get; private set; }
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject lostMenuUI;
    public GameObject wonMenuUI;
    public GameObject player;
    private bool lost = false;
    private bool won = false;

    void Awake()
    {
        if (pauseMenu != null && pauseMenu != this)
            Destroy(this);
        pauseMenu = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);

        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (lost || won) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void YouLost()
    {
        lost = true;
        lostMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

        public void YouWon()
    {
        won = true;
        wonMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        GameEnviroment.Reset();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        GameEnviroment.Reset();
        SceneManager.LoadScene("MainMenu");
    }
}