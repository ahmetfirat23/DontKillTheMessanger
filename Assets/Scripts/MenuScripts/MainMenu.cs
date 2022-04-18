using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
///
///</summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource clickSound;

    public void PlayTutorial()
    {
        GameLoader("Level0");
    }
    
    public void LoadMainMenu()
    {
        GameLoader("MainMenu");
    }
    
    
    
    public void LoadPlayer()
    { 

        ///TODO Save system

    }

    public void GameLoader(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

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
}
