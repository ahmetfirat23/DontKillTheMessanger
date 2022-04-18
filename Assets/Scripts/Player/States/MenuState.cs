using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : PlayerState
{
    public MenuState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Time.timeScale = 0;
        player.PauseMenu.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        
        Time.timeScale = 1;
        player.PauseMenu.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState && !player.InputHandler.PauseInput)
        {
            stateMachine.ChangeState(player.StateMachine.PrevState);
        }
    }
}