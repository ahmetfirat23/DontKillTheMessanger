using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TalkingState : PlayerState
{   
    public TalkingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
        
    public override void Enter()
    {
        base.Enter();
        player.InputHandler.customInteractionEvent = player.tempFunction;
        player.SetVelocityZero();
        Debug.Log("Enter Talking State");
            
    }
        
    public override void Exit()
    {
        base.Exit();
    }
        
    public override void LogicUpdate()
    {
        base.LogicUpdate();
            
        if(player.dialogueManager.finished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
            
    }
        
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}