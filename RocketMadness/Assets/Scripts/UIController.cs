using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    // Text components to display scores
    public TMP_Text currentScoreText;
    public TMP_Text lastScoreText;
    public TMP_Text bestScoreText;

    // Singleton instance of UIController
    public static UIController Instance { get; private set; }

    // Called when the script instance is being loaded
    void Awake()
    {
        Instance = this;
    }
	
    // Update the death screen with the final score and best score
    public void UpdateDeadScreen(int s)
	{
		// Get the high score from player preferences. If no score value exists, set it to 0.
		int highScore = PlayerPrefs.GetInt("High score", 0);

		// If the current score is greater than the stored high score, update the high score value.
		if (s > highScore)
		{
			highScore = s;
			PlayerPrefs.SetInt("High score", highScore);
		}

		// Update text fields to display the last score and the best score
		lastScoreText.text = "SCORE: " + s;
		bestScoreText.text = "BEST: " + highScore;
    }

	// Update the current score displayed on the UI
    public void UpdateScore(int s)
	{
		currentScoreText.text = s.ToString(); // Convert the score to a string and update the UI text
	}
}