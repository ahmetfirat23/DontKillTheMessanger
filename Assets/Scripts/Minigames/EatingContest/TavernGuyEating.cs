using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernGuyEating : MonoBehaviour
{
    bool foodOnThePlate = true;
    int currentFoodIndex = 0;
    public EatingContestMinigame miniGame;
    List<float> foodLeftTG = new List<float>();
    GameObject[] foodsForTG;
    Rigidbody2D rb;
    float LerpPct;
    float timePassed = 0;
    [SerializeField] float lerpDuration = 3f;
    bool nextOneComing = false;
    Vector2 currentFoodPos;
    public float eatingTime = 0.5f;
    float timePassedSinceLastBite = 0f;
    public float foodHP = 11f;
    int foodState = 0;

    public Sprite[] foodSpriteArr = new Sprite[3];

    void Start()
    {
        

        foodsForTG = GameObject.FindGameObjectsWithTag("FoodForTavernGuy");
        Debug.Log(foodsForTG.Length);
        for (int i = 0; i < foodsForTG.Length; i++)
        {
            foodLeftTG.Add(foodHP);
        }
    }
    
    void LerpTheFood()
    {

        LerpPct = timePassed / lerpDuration;
        foodsForTG[currentFoodIndex].transform.position = Vector2.Lerp(currentFoodPos, transform.position, LerpPct);
        timePassed += Time.deltaTime;
        if (Vector2.Distance(foodsForTG[currentFoodIndex].transform.position, transform.position) <= 0.05f)
        {
            nextOneComing = false;
            foodOnThePlate = true;
            timePassed = 0;
        }
    }

    void foodStateChange()
    {
        if (  8 > foodLeftTG[currentFoodIndex] && foodLeftTG[currentFoodIndex] > 4 || foodLeftTG[currentFoodIndex] < 0)
        {

            foodState++;
            foodsForTG[currentFoodIndex].GetComponent<SpriteRenderer>().sprite = foodSpriteArr[foodState];
        }
    }

    // Update is called once per frame
    void Update()
    {

        timePassedSinceLastBite += Time.deltaTime;
        if (foodLeftTG[currentFoodIndex] <= 0)
        {
            foodState = 0;
            miniGame.ThrowTheTrash(rb, foodsForTG, currentFoodIndex);
            currentFoodIndex++;
            if (foodsForTG.Length == currentFoodIndex)
            {
                miniGame.TGWins = true;
            }
            foodOnThePlate = false;
            nextOneComing = true;
            // play shout sound
            
            currentFoodPos = foodsForTG[currentFoodIndex].transform.position;
            
        }
        else if(foodOnThePlate && timePassedSinceLastBite > eatingTime)
        {
            eatingTime = Random.Range(0.5f, 0.8f);
            // play bite sound
            timePassedSinceLastBite = 0;
            foodLeftTG[currentFoodIndex] -= Mathf.Ceil(Random.Range(2, 4));
            foodStateChange();

        }
        if (nextOneComing)
        {
            LerpTheFood();
        }
        
    }
}
