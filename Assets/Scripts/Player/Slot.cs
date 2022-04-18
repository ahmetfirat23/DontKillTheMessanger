using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public SpriteRenderer firstRowSR;
    public Sprite firstRowSprite1;
    public Sprite firstRowSprite2;
    public SpriteRenderer secondRowSR;
    public Sprite secondRowSprite1;
    public Sprite secondRowSprite2;

    public GameObject firstRow;
    public GameObject secondRow;
    public GameObject[] firstRowObjects;
    public GameObject[] secondRowObjects;
    [HideInInspector] public int currentFirstRowObject = 1;
    [HideInInspector] public int currentSecondRowObject = 1;

    [HideInInspector]public float imageLength;
    [HideInInspector]public float firstRowLength;
    [HideInInspector]public float secondRowLength;
    
    private void Start()
    {
        imageLength = firstRowObjects[0].GetComponent<SpriteRenderer>().bounds.size.x;
        firstRowLength = firstRowObjects.Length * imageLength;
        secondRowLength = secondRowObjects.Length * imageLength;
    }
}