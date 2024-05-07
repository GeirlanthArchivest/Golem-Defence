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

    private GameObject Boss;
    private bool BossSpawned;

    public Button pauseButton;

    private GameObject Player1;
    private GameObject Player2;

    void Update()
    {
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");

        Boss = GameObject.FindWithTag("Boss");
        BossSpawned = EnemySpawner.bossSpawned;

        if (Player1 == null && Player2 == null)
        {
            PlayerPrefs.SetInt("Player1Score", PlayerMovement.score);
            PlayerPrefs.SetInt("Player1Score", Player2Movement.score);
            PlayerPrefs.SetFloat("TimeSpent", GameManager.currentTime);
            LoadMenu("DeathScreen");
        }
        else if (BossSpawned == true && Boss == null)
        {
            PlayerPrefs.SetInt("Player1Score", PlayerMovement.score);
            PlayerPrefs.SetInt("Player1Score", Player2Movement.score);
            PlayerPrefs.SetFloat("TimeSpent", GameManager.currentTime);
            LoadMenu("WinScreen");
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