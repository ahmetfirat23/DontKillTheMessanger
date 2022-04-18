using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Action<InputAction.CallbackContext> customInteractionEvent;

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool CodexInput { get; private set; }
    public bool PauseInput { get; private set; }


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
        customInteractionEvent = DummyFunction;

    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }
        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
    }
    
    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        customInteractionEvent(context);
    }
    
    public void OnOpenCodexInput(InputAction.CallbackContext context)
    {
        CodexInput = CodexInput ? false : true;
    }
    
    public void OnOpenCodexInput()
    {
        CodexInput = CodexInput ? false : true;
    }

    public void CloseCodexInput()
    {
        CodexInput = false;
    }
    
    public void OnPauseInput(InputAction.CallbackContext context)
    {
        PauseInput = PauseInput ? false : true;
    }

    public void OnContinueInput()
    {
        PauseInput = false;
    }

    private void DummyFunction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("bye champ");
        }
    }
}
