using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    public int id;
    public string name;
    public Sprite[] sprites;
    public Emotions emotion = Emotions.Neutral;
    public bool hasResponses = false;
    public Response[] responses;

}