using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pausePanel;
    public GameObject optionPanel;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                if (optionPanel.activeSelf)
                {
                    CloseOption();
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        optionPanel.SetActive(false);
        pausePanel.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenOption()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        optionPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}