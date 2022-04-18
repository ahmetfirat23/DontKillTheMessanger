using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EatingContestMinigame : MonoBehaviour
{
  
    
    bool nextOneComing = false;
    bool foodOnThePlate = true;

    EatingMinigameManager gameManager;

    public float EatInput
    {
        get; private set;
    }
    float timePassed = 0;
    [SerializeField]
    float lerpDuration = 2f;
    [SerializeField]
    float LerpPct = 0.8f;

    PlayerInput playerInput;
    float temp = 0;
    int currentFoodIndex = 0;
    GameObject[] foods;

    public bool TGWins = false;
    public bool CypherWins = false;

    Rigidbody2D rb;
    List<int> foodLeft = new List<int>();
    public int foodHPMin = 15;
    public int foodHPMax = 50;

    SpriteRenderer spriteRendererA;
    SpriteRenderer spriteRendererD;
    SpriteRenderer spriteRendererE;

    public Sprite spritePressedA;
    public Sprite spriteNeutralA;
    public Sprite spritePressedD;
    public Sprite spriteNeutralD;
    public Sprite spritePressedE;
    public Sprite spriteNeutralE;

    int foodState = 0;
    Vector2 currentFoodPos;
    

    public Sprite[] foodSpriteArray = new Sprite[3];

    // Start is called before the first frame update
    void Start()
    {
        
        spriteRendererA = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRendererD = transform.GetChild(1).GetComponent<SpriteRenderer>();
        spriteRendererE = transform.GetChild(2).GetComponent<SpriteRenderer>();
        spriteRendererE.enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<EatingMinigameManager>();
        playerInput = GetComponent<PlayerInput>();
        foods = GameObject.FindGameObjectsWithTag("Food");
        Debug.Log(foods.Length + " Food length");
        for(int i=0; i < foods.Length; i++)
        {
            
            foodLeft.Add(Random.Range(foodHPMin,foodHPMax));
            
        }
    }
   
    public void OnEatInput(InputAction.CallbackContext context)
    {
        if (this.enabled) { 
            if (foodOnThePlate)
            {
                EatInput = context.ReadValue<float>();
                
                //
                if (EatInput != temp && EatInput != 0)
                {
                    // light the keys
                
                
                    foodLeft[currentFoodIndex]--;
                    foodStateChange();
                }
                if (EatInput != 0)
                    temp = EatInput;
        
                if (EatInput == -1)
                {
                    spriteRendererA.sprite = spritePressedA;
                    spriteRendererD.sprite = spriteNeutralD;
                    //play press audio
                }
                else if (EatInput == 1)
                {
                    spriteRendererA.sprite = spriteNeutralA;
                    spriteRendererD.sprite = spritePressedD;
                    //play press audio
                }
                else
                {
                    spriteRendererA.sprite = spriteNeutralA;
                    spriteRendererD.sprite = spriteNeutralD;
                }
            }
        }

    }

    public void OnBellInput(InputAction.CallbackContext context)
    {
        if (this.enabled) { 
            if (!foodOnThePlate)
            {
                bool bellPressed = context.ReadValueAsButton();
                if (bellPressed && !nextOneComing)
                {
                    // shout sound
                    ManageButtonRenderers();
                    nextOneComing = true;
                    spriteRendererA.sprite = spriteNeutralA;
                    spriteRendererD.sprite = spriteNeutralD;
                }
            }
        }
    }
    void ManageButtonRenderers()
    {
        spriteRendererA.enabled = !spriteRendererA.enabled;
        spriteRendererD.enabled = !spriteRendererD.enabled;
        spriteRendererE.enabled = !spriteRendererE.enabled;
    }
    void LerpTheFood()
    {
        LerpPct = timePassed / lerpDuration;
        foods[currentFoodIndex].transform.position = Vector2.Lerp(currentFoodPos, transform.position, LerpPct);
        timePassed += Time.deltaTime;
        if (Vector2.Distance(foods[currentFoodIndex].transform.position, transform.position) <= 0.05f)
        {
            nextOneComing = false;
            foodOnThePlate = true;
            timePassed = 0;
        }
    }

    int CoefficientMaker()
    {
        if(Random.value < 0.5f)
        {
            return 1;
        }
        return -1;
    }
    public void ThrowTheTrash(Rigidbody2D rb2d,GameObject[] foodsArr, int index)
    {
        rb2d = foodsArr[index].GetComponent<Rigidbody2D>();
        Vector2 randomthrowVelocity = new Vector2(Random.Range(10, 30) * CoefficientMaker(), Random.Range(7,20));
        rb2d.isKinematic = false;
        rb2d.AddForce(randomthrowVelocity, ForceMode2D.Impulse);
        // play throw sound
        Destroy(rb2d.gameObject, 2f); // ?
    }

    void foodStateChange()
    {
        if( foodLeft[currentFoodIndex] == 8 || foodLeft[currentFoodIndex] == 0)
        {
            
            foodState++;
            foods[currentFoodIndex].GetComponent<SpriteRenderer>().sprite = foodSpriteArray[foodState];
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
        if(foodLeft[currentFoodIndex] <= 0)
        {
            foodState = 0;

            ThrowTheTrash(rb, foods, currentFoodIndex);
            currentFoodIndex++;
            if (currentFoodIndex >= foods.Length)
            {
                Debug.Log("You win");
                CypherWins = true;
                gameManager.CWins();
            }
   
            
            temp = 0;
            currentFoodPos = foods[currentFoodIndex].transform.position;
            // disable input
            foodOnThePlate = false;
            spriteRendererD.color = Color.white;
            spriteRendererA.color = Color.white;
            ManageButtonRenderers();
        }
        
        if (!CypherWins && TGWins)
        {
            gameManager.TGWins();
            Debug.Log("You lose");
        }

        if (nextOneComing)
        {
            LerpTheFood();
        }

    }
    
}
