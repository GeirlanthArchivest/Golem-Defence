using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace
using UnityEngine.SceneManagement; // Import the UnityEngine.SceneManagement namespace
using UnityEngine.UI; // Import the UnityEngine.UI namespace

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Static variable to track if the game is paused

    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public GameObject normalUI; // Reference to the normal game UI

    private GameObject Boss; // Reference to the Boss GameObject
    private bool BossSpawned; // Flag to check if the Boss has spawned

    public Button pauseButton; // Reference to the pause button

    private GameObject Player1; // Reference to Player 1
    private GameObject Player2; // Reference to Player 2

    // Update is called once per frame
    void Update()
    {
        // Find Player 1 and Player 2 by their tags
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");

        // Find the Boss by its tag and check if it has spawned
        Boss = GameObject.FindWithTag("Boss");
        BossSpawned = EnemySpawner.bossSpawned;

        // Check if both players are null, meaning they are dead
        if (Player1 == null && Player2 == null)
        {
            // Save the time spent in PlayerPrefs
            PlayerPrefs.SetFloat("TimeSpent", GameManager.currentTime);
            // Load the death screen
            LoadMenu("DeathScreen");
        }
        // Check if the Boss was spawned and now is null, meaning it was defeated
        else if (BossSpawned == true && Boss == null)
        {
            // Save the players' scores and time spent in PlayerPrefs
            PlayerPrefs.SetInt("Player1Score", PlayerMovement.score);
            PlayerPrefs.SetInt("Player2Score", Player2Movement.score); // Corrected to Player2Score
            PlayerPrefs.SetFloat("TimeSpent", GameManager.currentTime);
            // Load the win screen
            LoadMenu("WinScreen");
        }

        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
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

    // Resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        normalUI.SetActive(true); // Show the normal game UI
        Time.timeScale = 1f; // Resume game time
        GameIsPaused = false; // Update pause state
    }

    // Pause the game
    void Pause()
    {
        normalUI.SetActive(false); // Hide the normal game UI
        pauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Pause game time
        GameIsPaused = true; // Update pause state
        pauseButton.Select(); // Select the pause button
    }

    // Load a specified scene
    public void LoadMenu(string targetSceneName)
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene(targetSceneName); // Load the target scene
    }

    // Quit the game application
    public void QuitGame()
    {
        Debug.Log("Quitting game..."); // Log quitting message
        Application.Quit(); // Quit the application
    }
}
