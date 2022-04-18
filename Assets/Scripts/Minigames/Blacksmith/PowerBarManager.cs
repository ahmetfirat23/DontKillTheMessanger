using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerBarManager : MonoBehaviour
{
    Transform position;
    public static GameLoopManager manager;
    Rigidbody2D rigid;
    
    float speed = 10;
    bool isJumped = false;
    bool pressed = false;
    public bool isWin = false;
    public Transform point;
    public Vector2 size;
    public LayerMask layer;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, speed);
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pressed && !isJumped)
        {
            isJumped = true;
            speed = 0;
            CheckWin();
            pressed = false;
            // durdurma audio
        }
        
        
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started) pressed = true;
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(0, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed *= -1;
    }

    void CheckWin()
    {
        isWin = Physics2D.OverlapArea(new Vector2(4,2), new Vector2(6,1)) == GetComponent<Collider2D>();

        if (isWin){
            print("Bravo");
            System.Threading.Thread.Sleep(250);
            manager.WinUtility();
        }
        else
        {
            print("hakkýn gitti");
            manager.Health -= 1;
            System.Threading.Thread.Sleep(500);
            Restart();
            manager.LoseUtility();
        }

    }

    

    void Restart()
    {
        rigid.position = position.position;
        speed = 5;
        isJumped = false;
    }

}
