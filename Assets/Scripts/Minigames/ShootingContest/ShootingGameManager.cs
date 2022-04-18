using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingGameManager : MonoBehaviour
{
    public Canvas TutorialScreen;
    public Canvas WinScreen;
    public Canvas LoseScreen;
    public Canvas GameUI;
    public BowScript bowScript;

    int applesShot = 0;
    int livesLeft = 5;

    int appleAmount;


    private void Start()
    {
        Time.timeScale = 0;
        appleAmount = GameObject.FindGameObjectsWithTag("Apple").Length;
    }
    public void TutorialEnd()
    {
        Time.timeScale = 1;
        bowScript.enabled = true;
        TutorialScreen.enabled = false;
        GameUI.enabled = true;
    }
    
    public void YouLost()
    {
        Time.timeScale = 0.5f;
        bowScript.enabled = false;

        LoseScreen.enabled = true;
    }

    public void YouWin()
    {
        Time.timeScale = 0.5f;
        bowScript.enabled = false;

        WinScreen.enabled = true;
    }
    public void IncreaseAppleShot()
    {
        applesShot++;
    }
    public void DecreaseLivesLeft()
    {
        Destroy(GameUI.transform.GetChild(0).gameObject);
        livesLeft--;
    }

    public void RestartMiniGame()
    {
        SceneManager.LoadScene("ShootingMinigame");
    }



    public void LoadMainScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Baker_Minigame");
    }
    private void Update()
    {
        if(applesShot == appleAmount)
        {
            YouWin();
        }
        else if(livesLeft <= 0)
        {
            YouLost();
        }
    }




}
