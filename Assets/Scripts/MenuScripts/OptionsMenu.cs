using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/// <summary>
///
///</summary>
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer sfxAudioMixer;
    public AudioSource clickSound;
    

    private void Start()
    {
        Button[] btn = FindObjectsOfType<Button>();
        foreach (Button b in btn)
        {
            b.onClick.AddListener(Onclick);
        }
    }
    void Onclick()
    {
        clickSound.Play();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }    
    
    public void SetSFXVolume(float volume)
    {
        sfxAudioMixer.SetFloat("volume", volume);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
    
    public void ButtonClick()
    {
        clickSound.Play();
    }
}
