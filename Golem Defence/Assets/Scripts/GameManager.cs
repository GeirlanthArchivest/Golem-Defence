using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public static GameManager instance;

    public static float currentTime = 0f;
    public TextMeshProUGUI timerText;

    public bool menuScene = false;

    private void Awake()
    {
        currentTime = 0f;

        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Time.timeScale != 0f || menuScene == false)
        {
            currentTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        // Format the timer value as minutes and seconds
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        // Update the UI Text component
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
