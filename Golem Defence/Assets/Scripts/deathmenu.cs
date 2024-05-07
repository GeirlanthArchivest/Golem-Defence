using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class deathmenu : MonoBehaviour
{
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;
    public TextMeshProUGUI timeText;


    // Start is called before the first frame update
    void Start()
    {
        int score1 = PlayerPrefs.GetInt("Player1Score", 0); // Default value is 0 if the key doesn't exist
        int score2 = PlayerPrefs.GetInt("Player2Score", 0);
        float timeSpent = PlayerPrefs.GetFloat("TimeSpent", 0f);

        player1Text.text = "Player 1 Score: " + score1;
        player2Text.text = "Player 2 Score: " + score2;
        // Format the timer value as minutes and seconds
        float minutes = Mathf.FloorToInt(timeSpent / 60);
        float seconds = Mathf.FloorToInt(timeSpent % 60);

        // Update the UI Text component
        timeText.text = string.Format("Total Time Spent: {0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTimerUI()
    {
        
    }
}
