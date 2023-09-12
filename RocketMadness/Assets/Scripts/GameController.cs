using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    // Text component to display the score
    public TMP_Text scoreText;

    // Reference to the spawner
    private Spawner spawner;

    // Current score
    public int score;

    // Singleton instance of GameController
    public static GameController Instance { get; private set; }

    // Called when the script instance is being loaded
    void Awake()
    {
        Instance = this;

        // Get the reference to the spawner
        spawner = Spawner.Instance;
        spawner.enabled = false;
    }

    // Called once per frame
    void Update()
    {
        // Enable the spawner if the player is not dead
        if (!(PlayerController.Instance.dead))
        {
            spawner.enabled = true;
        }
        else
        {
            spawner.enabled = false;
        }
    }

    // Increase the score by 1
    public void IncreaseScore()
	{  
		score++;
		UIController.Instance.UpdateScore(score);
	}

    // Restart the game
    public void RestartGame()
	{
		PlayerController.Instance.Restart();
        
		NewMap();
        scoreText.text = "0";
		score = 0;
	}

    // Get the current score
    public int GetScore()
	{
		return score;
	}

    // Remove all obstacles in the scene
    public void NewMap()
    {
        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();

        for (int i = 0; i < obstacles.Length; i++) 
        {
            Destroy(obstacles[i].gameObject);
        }
    }
}
