using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject normalUI;
    public GameObject deathUI;

    public Button deathButton;
    public Button pauseButton;

    private GameObject Player1;
    private GameObject Player2;

    void Update()
    {
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");

        if (Player1 == null && Player2 == null)
        {
            LoadMenu("DeathScreen");
        }

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
        normalUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        normalUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseButton.Select();
    }
    public void LoadMenu(string targetSceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(targetSceneName);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}