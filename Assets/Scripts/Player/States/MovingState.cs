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
    
    public override void DoChecks()
    {
        base.DoChecks();
    }
    
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Moving State");    
    }
    
    public override void Exit()
    {
        base.Exit();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        xInput = player.InputHandler.NormInputX;
        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(playerData.movementSpeed * xInput);

        if (!isExitingState && player.triggerEntered &&((!player.dialogueManager.started && !player.dialogueManager.finished) ||
                                                        (!player.dialogueManager.started && player.dialogueManager.finished)))
        {
            player.InputHandler.customInteractionEvent = TriggerDialogue;
        }

        
        if (!isExitingState && xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    private void TriggerDialogue(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            stateMachine.ChangeState(player.TalkingState);
            player.tempObj.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
    
}