using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingState : PlayerState
{
    protected int xInput;
    public MovingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Moving State");    
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        xInput = player.InputHandler.NormInputX;
        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(playerData.movementSpeed * xInput);
        if (!isExitingState)
        {
            if (player.InputHandler.PauseInput)
            {
                player.StateMachine.ChangeState(player.MenuState);
            }

            if (player.triggerEntered && player.dialogueManager!=null &&
                ((!player.dialogueManager.started && !player.dialogueManager.finished) ||
                 (!player.dialogueManager.started && player.dialogueManager.finished)))
            {
                player.InputHandler.customInteractionEvent = TriggerDialogue;
            }
            
            if (player.InputHandler.CodexInput)
            {
                stateMachine.ChangeState(player.DecryptingState);
            }

            else if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    private void TriggerDialogue(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (player.dialogueManager != null)
            {
                stateMachine.ChangeState(player.TalkingState);
                player.tempObj.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
            else
            {
                stateMachine.ChangeState(player.DecryptingState);
            }
        }
    }
    
}