using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Singleton instance of AudioManager
    public static AudioManager Instance { get; private set; }

    // Arrays to hold music and sound effects
    public Sound[] musicSounds, sfxSounds;

    // Audio sources for music and sound effects
    public AudioSource musicSource, sfxSource;

    // Initial volume setting
    public float startVolume;

    // Maximum value for the volume slider
    [SerializeField] private float maxSliderAmount = 100.0f;

    // UI Text components for displaying volume values
    [SerializeField] private TMP_Text musicVolumeTextUI = null;
    [SerializeField] private TMP_Text sfxVolumeTextUI = null;

    // Called when the script instance is being loaded
    private void Awake()
    {
        Instance = this;
        startVolume = 0.5f;
    }

    // Called on script start
    public void Start()
    {
        // LoadValues();
        PlayMusic("Waiting");
    }

    // Adjust music volume based on slider value
    public void MusicSlider(float value)
    {
        float musicValue = value * maxSliderAmount;
        musicVolumeTextUI.text = musicValue.ToString("0");
        musicSource.volume = musicValue / 100;
        PlayerPrefs.SetFloat("MusicVolume", musicValue);
    }

    // Adjust sound effects volume based on slider value
    public void SFXSlider(float value)
    {
        float sfxValue = value * maxSliderAmount;
        sfxVolumeTextUI.text = sfxValue.ToString("0");
        sfxSource.volume = sfxValue / 100;
        PlayerPrefs.SetFloat("SFXVolume", sfxValue);
    }

    // Play the specified music track
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found.");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    // Play the specified sound effect
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found.");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
