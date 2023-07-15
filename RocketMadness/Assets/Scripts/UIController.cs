using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text lastScoreText;
    public TMP_Text bestScoreText;

    public static UIController Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDeadScreen(int s)
	{
        int num = 0;
		// int num = SaveManager.Instance.state.best;
		if (s > num)
		{
			num = s;
		// 	SaveManager.Instance.state.best = s;
		// 	SaveManager.Instance.Save();
		}
		// CheckUnlocks(s);
		// UpdateCoins();
		lastScoreText.text = "SCORE: " + s;
		bestScoreText.text = "BEST: " + num;
    }

    public void UpdateScore(int s)
	{
		currentScoreText.text = string.Concat(s);
		// scorePop.Pop();
		// AudioManager.Instance.Play("Score");
	}
}