using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public GameObject Main;
    public GameObject Start;
    public GameObject Settings;
    public GameObject Dead;
    public GameObject Score;

    public bool isReady = false;

    public static ScreenController Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        MainUI();
    }

    void Update()
    {
        if(isReady)
        {
            PlayerController.Instance.rb.gravityScale = 0;
            if (Input.anyKey)
            {
                PlayerController.Instance.rb.gravityScale = PlayerController.Instance.gravity;
                GameplayUI();
            }
        }
    }

    public void MainUI()
    {
        Main.SetActive(true);
        Start.SetActive(false);
        Settings.SetActive(false);
        Dead.SetActive(false);
        Score.SetActive(false);

        PlayerController.Instance.dead = true;
        isReady = false;

    } 

    public void StartUI()
    {
        Main.SetActive(false);
        Start.SetActive(true);
        Settings.SetActive(false);
        Dead.SetActive(false);
        Score.SetActive(false);

        isReady = true;
        PlayerController.Instance.dead = true;
    } 

    public void SettingsUI()
    {
        Main.SetActive(false);
        Start.SetActive(false);
        Settings.SetActive(true);
        Dead.SetActive(false);
        Score.SetActive(false);

        PlayerController.Instance.dead = true;
        isReady = false;
    }

    public void DeadUI()
    {
        Main.SetActive(false);
        Start.SetActive(false);
        Settings.SetActive(false);
        Dead.SetActive(true);
        Score.SetActive(false);

        PlayerController.Instance.dead = true;
        isReady = false;
    }

    public void GameplayUI()
    {
        Main.SetActive(false);
        Start.SetActive(false);
        Settings.SetActive(false);
        Dead.SetActive(false);
        Score.SetActive(true);

        PlayerController.Instance.dead = false;
        isReady = false;
    }

    public void Exit()
    {
        Application.Quit(1);
    }
}