using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    #region Variables
    public TMP_Text scoreText;

    private Spawner spawner;

    public bool gameActive;
    public int score;

    public static GameController Instance { get; private set; }

    #endregion

    #region Main Methods
    void Awake()
    {
        Instance = this;

        spawner = Spawner.Instance;
        spawner.enabled = false;
        
        gameActive = false;
    }

    void Update()
    {
        if(!(PlayerController.Instance.dead))
        {
            spawner.enabled = true;
        }
        else
        {
            spawner.enabled = false;
        }
    }
    #endregion

    #region Helper Methods

    public void IncreaseScore()
	{
		score++;
		UIController.Instance.UpdateScore(score);
	}

    public void RestartGame()
	{
		PlayerController.Instance.Restart();
        
		NewMap();
        scoreText.text = "0";
		score = 0;
		// Time.timeScale = 1f;
		// score.text = "0";
		// r = 0f;
		// r2 = 0f;
		// watchAdBtn.SetActive(value: true);
		// CameraMovement.Instance.SetStart();
	}

    public int GetScore()
	{
		return score;
	}

    public void NewMap()
    {
        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();

        for (int i = 0; i < obstacles.Length; i++) 
        {
            Destroy(obstacles[i].gameObject);
        }
    }
    #endregion
}
