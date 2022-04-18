using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class IdleState : PlayerState
{
    protected int xInput;
    
    public IdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();
        Debug.Log("Entering Idle State");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        if (!isExitingState)
        {
            if (player.InputHandler.PauseInput)
            {
                player.StateMachine.ChangeState(player.MenuState);
            }
            
            if (player.triggerEntered && player.dialogueManager!=null &&
                (!player.dialogueManager.started && !player.dialogueManager.finished ||
                 !player.dialogueManager.started && player.dialogueManager.finished))
            {
                player.InputHandler.customInteractionEvent = TriggerDialogue;
            }
            else if (player.triggerEntered)
            {
                player.InputHandler.customInteractionEvent = TriggerCheck;
            }

            
            if (player.InputHandler.CodexInput)
            {
                stateMachine.ChangeState(player.DecryptingState);
            }

            
            else if (xInput != 0)
            {
                stateMachine.ChangeState(player.MovingState);
            }
        }


    }

    private void TriggerDialogue(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            stateMachine.ChangeState(player.TalkingState);
            player.tempObj.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void TriggerCheck(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.InputHandler.OnOpenCodexInput();
            stateMachine.ChangeState(player.DecryptingState);
            player.CheckingManager.SetActive(true);
        }
    }
}