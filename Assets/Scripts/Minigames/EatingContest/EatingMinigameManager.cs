using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatingMinigameManager : MonoBehaviour
{
    public EatingContestMinigame minigame;
    public TavernGuyEating tgEating;
    public Canvas TGWinsScreen;
    public Canvas CypherWinsScreen;
    public Canvas Tutorial;
    
    private void Awake()
    {
        Time.timeScale = 0;
    }
    


    public void TutorialEnd()
    {
        Tutorial.enabled = false;
        Time.timeScale = 1;
        tgEating.enabled = true;
        minigame.enabled = true;
    }
    
    public void TGWins()
    {
        Time.timeScale = 0.3f;
        Debug.Log("tgwins");
        minigame.enabled = false;
        tgEating.enabled = false;
        TGWinsScreen.enabled = true;
        
    }
    public void CWins()
    {
        Time.timeScale = 0.3f;
        minigame.enabled = false;
        tgEating.enabled = false;
        CypherWinsScreen.enabled = true;
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ShootingMinigame");
    }
    public void Restart()
    {
        SceneManager.LoadScene(sceneName: "Minigame_EatingContest");
    }
}
