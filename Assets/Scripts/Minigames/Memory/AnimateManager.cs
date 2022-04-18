using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateManager : MonoBehaviour
{
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
    }
    public void Light(Color color)
    {
        // ses efekti konabilir
        Color oldColor = new Color(71,71,70);
        renderer.color = color;
        StartCoroutine(Sleep(oldColor));
    }
    public void LightYourChoice(Color color)
    {
        Color oldColor = renderer.color;
        renderer.color = color;
    }

    IEnumerator Sleep(Color color)
    {
        yield return new WaitForSeconds(.5f);
        renderer.color = color;
    }
}
