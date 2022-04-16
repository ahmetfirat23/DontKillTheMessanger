using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue")]

public class DialogueData : ScriptableObject
{
    public Line[] lines;
}