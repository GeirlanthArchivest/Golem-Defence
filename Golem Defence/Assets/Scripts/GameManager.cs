using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Array of player GameObjects
    public GameObject[] players;

    // Static reference to the GameManager instance
    public static GameManager instance;

    // Current time elapsed in the game
    public static float currentTime = 0f;

    // Reference to the UI Text component for displaying the timer
    public TextMeshProUGUI timerText;

    // Boolean indicating whether the scene is a menu scene
    public bool menuScene = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the current time
        currentTime = 0f;

        // Check if the GameManager instance exists
        if (instance == null)
        {
            instance = this; // If not, set this as the instance
        }
        else
        {
            Destroy(gameObject); // If yes, destroy this instance
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the timer only if the game is not paused or in a menu scene
        if (Time.timeScale != 0f || menuScene == false)
        {
            currentTime += Time.deltaTime; // Increment the current time
            UpdateTimerUI(); // Update the timer UI
        }
    }

    // Update the UI text to display the timer
    void UpdateTimerUI()
    {
        // Format the timer value as minutes and seconds
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        // Update the UI Text component with the formatted time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
