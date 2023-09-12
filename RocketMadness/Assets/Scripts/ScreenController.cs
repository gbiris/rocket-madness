using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenController : MonoBehaviour
{
    // References to different UI screens
    public GameObject mainMenuScreen;
    public GameObject readyScreen;
    public GameObject settingsScreen;
    public GameObject deadScreen;
    public GameObject gameplayScreen;

    // Flag to indicate if the player is ready to start playing
    public bool isReady = false;

    // Singleton instance of ScreenController
    public static ScreenController Instance { get; set; }

    // Called when the script instance is being loaded
    private void Awake()
    {
        Instance = this;

        // Initialize the UI to the main menu screen
        MainUI();
    }

    // Called every frame
    void Update()
    {
        // If the player is ready to start
        if (isReady)
        {
            // Set player's gravity based on user input
            PlayerController.Instance.rb.gravityScale = 0;
            if (Input.anyKey)
            {
                PlayerController.Instance.rb.gravityScale = PlayerController.Instance.gravity;
                GameplayUI();
                AudioManager.Instance.PlayMusic("Background");
            }
        }
    }

    // Set the UI to the main menu screen
    public void MainUI()
    {
        mainMenuScreen.SetActive(true);
        readyScreen.SetActive(false);
        settingsScreen.SetActive(false);
        deadScreen.SetActive(false);
        gameplayScreen.SetActive(false);

        // Set player's death state and readiness
        PlayerController.Instance.dead = true;
        isReady = false;
    } 

    // Set the UI to the ready screen
    public void ReadyScreenUI()
    {
        mainMenuScreen.SetActive(false);
        readyScreen.SetActive(true);
        settingsScreen.SetActive(false);
        deadScreen.SetActive(false);
        gameplayScreen.SetActive(false);

        // Set player's readiness and death state
        isReady = true;
        PlayerController.Instance.dead = true;
    } 

    // Set the UI to the settings screen
    public void settingsScreenUI()
    {
        mainMenuScreen.SetActive(false);
        readyScreen.SetActive(false);
        settingsScreen.SetActive(true);
        deadScreen.SetActive(false);
        gameplayScreen.SetActive(false);

        // Set player's death state and readiness
        PlayerController.Instance.dead = true;
        isReady = false;
    }

    // Set the UI to the dead screen
    public void DeadUI()
    {
        mainMenuScreen.SetActive(false);
        readyScreen.SetActive(false);
        settingsScreen.SetActive(false);
        deadScreen.SetActive(true);
        gameplayScreen.SetActive(false);

        // Set player's death state, play sound effects, and readiness
        PlayerController.Instance.dead = true;
        AudioManager.Instance.PlayMusic("Waiting");
        AudioManager.Instance.PlaySFX("Explode");
        isReady = false;
    }

    // Set the UI to the gameplay screen
    public void GameplayUI()
    {
        mainMenuScreen.SetActive(false);
        readyScreen.SetActive(false);
        settingsScreen.SetActive(false);
        deadScreen.SetActive(false);
        gameplayScreen.SetActive(true);

        // Set player's death state and readiness
        PlayerController.Instance.dead = false;
        isReady = false;
    }

    // Exit the application
    public void Exit()
    {
        Application.Quit(1);
    }
}