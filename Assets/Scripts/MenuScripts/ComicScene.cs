using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("Level1");
    }
}
