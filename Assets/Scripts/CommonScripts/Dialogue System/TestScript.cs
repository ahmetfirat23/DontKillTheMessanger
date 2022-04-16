using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TestScript : MonoBehaviour
{
    private DialogueManager dm;

    private Action<InputAction.CallbackContext> tempFunction;
    private GameObject tempObj;

    private bool entered = false;

    private void Start()
    {
        dm = GetComponent<DialogueManager>();
    }

    private void Update()
    {
        if (entered)
        {
            if ((!dm.started && !dm.finished) || (!dm.started && dm.finished))
            {
                tempObj.GetComponent<PlayerInputHandler>().customInteractionEvent = FirstFunc;
            }
            else
            {
                tempObj.GetComponent<PlayerInputHandler>().customInteractionEvent = tempFunction;
            }
        }
    }

    private void FirstFunc(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        entered = true;
        tempObj = collider.gameObject;
        tempFunction = tempObj.GetComponent<PlayerInputHandler>().customInteractionEvent;
        

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        entered = false;
        collider.gameObject.GetComponent<PlayerInputHandler>().customInteractionEvent = tempFunction;
    }
}
