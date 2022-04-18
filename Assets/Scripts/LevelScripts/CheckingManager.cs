using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckingManager : MonoBehaviour
{
    public Animator Anim;
    public int firstCorrectRow;
    public int secondCorrectRow;
    public Slot slot;
    public GameObject checkerNPC;
    public string NextLevelName;
    
    
    public void CheckCodex()
    {
        Debug.Log(slot.currentFirstRowObject+" "+slot.currentSecondRowObject);
        if(slot.currentFirstRowObject == firstCorrectRow && slot.currentSecondRowObject == secondCorrectRow)
        {
            StopAllCoroutines();
            StartCoroutine(CorrectCodex());
        }
        else
        {
            StartCoroutine(FalseCodex());
        }
    }

    public IEnumerator CorrectCodex()
    {
       // checkerNPC.GetComponent<Animator>().Play("correct");
        yield return new WaitForSeconds(0.01f);
        GameLoader(NextLevelName);
    }
    
    public IEnumerator FalseCodex()
    {
        //checkerNPC.GetComponent<Animator>().Play("false");
        yield return new WaitForSeconds(0.01f);
    }
    
    public void GameLoader(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
