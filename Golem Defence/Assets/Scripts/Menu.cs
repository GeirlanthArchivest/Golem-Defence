using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace
using UnityEngine.EventSystems; // Import the UnityEngine.EventSystems namespace
using UnityEngine.SceneManagement; // Import the UnityEngine.SceneManagement namespace

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu; // Reference to the main menu GameObject
    [SerializeField] GameObject instructions; // Reference to the instructions menu GameObject
    //[SerializeField] GameObject levelSelect; // Reference to the level select menu GameObject (commented out)
    [SerializeField] GameObject backButton; // Reference to the back button GameObject
    [SerializeField] GameObject playButton; // Reference to the play button GameObject

    // Method to load a new scene
    public void PlayGame(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName); // Load the specified scene
    }

    // Method to show the instructions menu
    public void Instructions()
    {
        mainMenu.SetActive(false); // Hide the main menu
        instructions.SetActive(true); // Show the instructions menu
        EventSystem.current.SetSelectedGameObject(backButton.gameObject); // Set the back button as the selected UI element
    }

    // Method to go back to the main menu from the instructions menu
    public void Back()
    {
        mainMenu.SetActive(true); // Show the main menu
        instructions.SetActive(false); // Hide the instructions menu
        EventSystem.current.SetSelectedGameObject(playButton.gameObject); // Set the play button as the selected UI element
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("QUIT!"); // Log a quit message
        Application.Quit(); // Quit the application
    }
}
