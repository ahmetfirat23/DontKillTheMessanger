using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject firstRow;
    public GameObject secondRow;
    public GameObject[] firstRowObjects;
    public GameObject[] secondRowObjects;
    [HideInInspector] public int currentFirstRowObject;
    [HideInInspector] public int currentSecondRowObject;

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