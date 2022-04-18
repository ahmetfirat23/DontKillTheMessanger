using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D body;
    Transform position;
    bool pressed = false;
    float Health = 3;
    float angSpeed = 90;
    float speed = 10;
    bool isJumped = false;


    int time = 150;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        position = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (isJumped)
        {
            time--;
            if (time <= 0)
            {
                GetComponent<GameManager>().GetDamage();
                Collision();
            }
        }
        else
        {
            time = 150;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isJumped)
        //{
        //    float input = Input.GetAxis("Horizontal");
        //    body.angularVelocity = -input * angSpeed;
        //}
            
        if (pressed)
        {
            body.angularVelocity = 0;
            Vector3 vector = Quaternion.ToEulerAngles(position.rotation);
            body.velocity = speed * new Vector2(-Mathf.Sin(vector.z), Mathf.Cos(vector.z));
            pressed = false;
            isJumped = true;
        }
    }
    public void OnRotateInput(InputAction.CallbackContext context)
    {
        if (!isJumped)
        {
            float input = context.ReadValue<float>();
            body.angularVelocity = -input * angSpeed;
        }
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started) pressed = true;
    }
    public void Collision()
    {
        body.velocity = new Vector2(0, 0);
        body.angularVelocity = 0;
        position.position = new Vector2(0, -4.6f);
        position.rotation = new Quaternion(0, 0, 0, 0);
        isJumped = false;
    }
}
