using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    public float Health = 3;
    public List<Object> sprites;
    private int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        PowerBarManager.manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinUtility()
    {
        StartCoroutine(LoadMainScene());
    }
    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("mainScene");
    }
    public void LoseUtility()
    {
        Destroy(sprites[sprites.Count-i]);
        i++;
        // can gitme sesi
        if (Health <= 0)
        {
            // TODO lose condition and audio
        }
        
    }
}
