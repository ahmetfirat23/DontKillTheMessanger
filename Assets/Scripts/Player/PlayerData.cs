using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public float movementSpeed = 5f;
    public float rotateInterval = 0.025f;
}