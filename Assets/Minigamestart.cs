using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigamestart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene("ShootingMinigame");
    }
}
