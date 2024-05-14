using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject instructions;
    //[SerializeField] GameObject levelSelect;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject playButton;

    public void PlayGame(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }

    public void Instructions()
    {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}