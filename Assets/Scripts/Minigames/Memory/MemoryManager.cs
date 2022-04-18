using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MemoryManager : MonoBehaviour
{
    float levelNumber = 1;
    AnimateManager[] animates;
    List<int> keys = new List<int>();
    List<int> sayilar = new List<int>();
    bool isCorrect = true;
    public float health = 3;
    public List<Object> sprites;
    private int i = 1;
    bool isPressing = false;
    public Canvas winScreen;
    // Start is called before the first frame update
    void Start()
    {
        winScreen.enabled = false;
        animates = GetComponentsInChildren<AnimateManager>();
        AnimateLevel(2 * levelNumber);
    }

    // Update is called once per frame
    void Update()
    {

        //LookForAnswer();
             
        if (keys.Count == 2 * levelNumber)
        {
            CheckAnswer();
            if (isCorrect)
            {
                levelNumber++;
                if (levelNumber == 4)
                {
                    ShowButton(); // 3 levelden sonra 4. levele geçmeden win kýsmý
                }
                AnimateLevel(2 * levelNumber);
                foreach(AnimateManager anim in animates)
                {
                    anim.Light(Color.green);
                }
                // doðru bildiniz ve audio 
            }
            else
            {
                keys.Clear();
                health--;
                isCorrect = true;
                GetDamage();
                // baþtan tekrar deneyin kýsmý ve audio
            }
        }
    }

    void ShowButton()
    {
        winScreen.enabled = true;
    }

    void CheckAnswer()
    {
        for (int i = 0; i < sayilar.Count; i++)
        {
            if (keys[i] != sayilar[i]+1)
            {
                isCorrect = false;
            }
        }
    }

    void AnimateLevel(float diff)
    {
        sayilar.Clear();
        keys.Clear();
        for (int i = 0; i < diff; i++)
        {
            int sayi  = Random.Range(0, 9);
            if (sayilar.Contains(sayi))
            {
                i--;
                continue;
            }
            else
            {
                sayilar.Add(sayi);
            }
        }

        
            StartCoroutine(Sleep());
               
    }


    public void OnOneInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            print("hi");
            keys.Add(1); animates[0].Light(Color.black);
        }
    }
    public void OnTwoInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(2); animates[1].Light(Color.black);
        }
    }
    public void OnThreeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(3); animates[2].Light(Color.black);
        }
    }
    public void OnFourInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(4); animates[3].Light(Color.black);
        }
    }
    public void OnFiveInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(5);
            animates[4].Light(Color.black);
        }
    }
    public void OnSixInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(6);
            animates[5].Light(Color.black);
        }
    }
    public void OnSevenInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(7);
            animates[6].Light(Color.black);
        }
    }
    public void OnEightInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(8);
            animates[7].Light(Color.black);
        }
    }
    public void OnNineInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keys.Add(9);
            animates[8].Light(Color.black);
        }
    }
    void LookForAnswer()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (!isPressing)
            {
                keys.Add(1);
                print(1);
            }
            
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (!isPressing)
            {
                keys.Add(2);print(2);
            }
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (!isPressing)
            {
                keys.Add(3);print(3);
            }
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            if (!isPressing)
            {
                keys.Add(4);print(4);
            }
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            if (!isPressing)
            {
                keys.Add(5);print(5);
            }
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            if (!isPressing)
            {
                keys.Add(6);print(6);
            }
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            if (!isPressing)
            {
                keys.Add(7);print(7);
            }
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            if (!isPressing)
            {
                keys.Add(8);print(8);
            }
            isPressing = true;
            
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            if (!isPressing)
            {
                keys.Add(9);print(9);
            }
            isPressing = true;
            
        }
        else
        {
            isPressing = false;
        }
    }

    IEnumerator Sleep()
    {
        foreach (int sayi in sayilar)
        {
            yield return new WaitForSeconds(1f);
            animates[sayi].Light(Color.black);
        }
        
    }

    public void GetDamage()
    {
        Destroy(sprites[sprites.Count - i]);
        i++;
        // can gitme sesi
        if (health <= 0)
        {
            LoseUtility();
        }
    }

    void LoseUtility()
    {
        print("kaybettiniz");
        
    }

    public void WinUtility()
    {
        StartCoroutine(Wait());
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("mainScene");
    }
}
