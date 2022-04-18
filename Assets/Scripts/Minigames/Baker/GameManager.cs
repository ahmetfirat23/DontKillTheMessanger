using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Object> sprites;
    public float Health = 3;
    private int i = 1;
    int time = 1500;
    public Canvas winScreen;
    public Canvas loseScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        time--;
        if (time<=0)
        {
            LoseUtility();
        }
    }

    public void GetDamage()
    {
        Destroy(sprites[sprites.Count - i]);
        i++;
        Health--;
        // can gitme sesi
        if (Health <= 0)
        {
            LoseUtility();
        }
    }

    void LoseUtility()
    {
        this.gameObject.SetActive(false);
        loseScreen.enabled = true;
    }
    public void LoadSameScene()
    {
        SceneManager.LoadScene("Memory_Minigame");
    }

    public void WinUtility()
    {
        winScreen.enabled = true;        
    }

    public void LoadScene()
    {
        StartCoroutine(LoadMainScene());

    }
    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("mainScene");
    }
}
