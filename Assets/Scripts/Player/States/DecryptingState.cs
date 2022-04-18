using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecryptingState : PlayerState
{
    private int xInput;
    private int yInput;
    private int imageToMove;
    private int rowIndex;
    private bool isRotating;
    
    public DecryptingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
    }
        
    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocityZero();
        player.Codex.GetComponent<Animator>().Play("CodexOpen");

    }
        
    public override void Exit()
    {
        base.Exit();
        player.Codex.GetComponent<Animator>().Play("CodexClose");
        Debug.Log("Exiting decrypting state");
    }
        
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        if (!isExitingState)
        {
            if (player.InputHandler.PauseInput)
            {
                player.StateMachine.ChangeState(player.MenuState);
            }
            
            if (yInput != 0 && !isRotating)
            {
                isRotating = true;
                player.StartCoroutine(Rotate(yInput));
            }
            else if (!isRotating && xInput > 0 && rowIndex == 0)
            {
                rowIndex = 1;
                player.Slot.firstRowSR.sprite = player.Slot.firstRowSprite1;
                player.Slot.secondRowSR.sprite = player.Slot.secondRowSprite2;
            }
            else if (!isRotating && xInput < 0 && rowIndex == 1)
            {
                rowIndex = 0;
                player.Slot.firstRowSR.sprite = player.Slot.firstRowSprite2;
                player.Slot.secondRowSR.sprite = player.Slot.secondRowSprite1;
            }

            else if (!player.InputHandler.CodexInput)
            {
                stateMachine.ChangeState(player.IdleState);
                if (player.CheckingManager.activeInHierarchy)
                {
                    player.CheckingManager.SetActive(false);
                }
            }
        }
    }

    private IEnumerator Rotate(int yInput)
    {
        Slot slot = player.Slot;
        if (rowIndex == 0)
        {
            if (yInput < 0)
            {
                imageToMove = slot.currentFirstRowObject == 0 ? slot.firstRowObjects.Length - 1 : slot.currentFirstRowObject - 1;
                slot.firstRowObjects[imageToMove].transform.position = 
                    new Vector2(slot.firstRowObjects[imageToMove].transform.position.x,
                        slot.firstRowObjects[imageToMove].transform.position.y-yInput * slot.firstRowLength);
            }
            
            for (int i = 0; i < 30; i++)
            {
                Vector2 pos = slot.firstRow.transform.position;
                pos = new Vector2(pos.x,
                    pos.y + yInput * slot.imageLength / 30);
                slot.firstRow.transform.position = pos;
                yield return new WaitForSeconds(playerData.rotateInterval);
            }

            if (yInput > 0){
                imageToMove = slot.currentFirstRowObject;
                slot.firstRowObjects[imageToMove].transform.position = 
                    new Vector2(slot.firstRowObjects[imageToMove].transform.position.x,
                        slot.firstRowObjects[imageToMove].transform.position.y-yInput * slot.firstRowLength);
            }
            

            if(slot.currentFirstRowObject==0 && yInput<0)
            {
                slot.currentFirstRowObject = slot.firstRowObjects.Length-1;
            }
            else if(slot.currentFirstRowObject==slot.firstRowObjects.Length-1 && yInput>0)
            {
                slot.currentFirstRowObject = 0;
            }
            else
            {
                slot.currentFirstRowObject += yInput;
            }
        }
        
        else
        {
            if (yInput < 0)
            {
                imageToMove = slot.currentSecondRowObject == 0 ? slot.secondRowObjects.Length - 1 : slot.currentSecondRowObject - 1;
                player.Slot.secondRowObjects[imageToMove].transform.position = 
                    new Vector2(slot.secondRowObjects[imageToMove].transform.position.x,
                        slot.secondRowObjects[imageToMove].transform.position.y-yInput * slot.secondRowLength);
            }
            
            for (int i = 0; i < 30; i++)
            {
                Vector2 pos = slot.secondRow.transform.position;
                pos = new Vector2(pos.x,
                    pos.y + yInput * slot.imageLength / 30);
                slot.secondRow.transform.position = pos;
                yield return new WaitForSeconds(playerData.rotateInterval);
            }
            
            if(yInput>0){
                imageToMove = slot.currentSecondRowObject;
                player.Slot.secondRowObjects[imageToMove].transform.position = 
                    new Vector2(slot.secondRowObjects[imageToMove].transform.position.x,
                        slot.secondRowObjects[imageToMove].transform.position.y-yInput * slot.secondRowLength);
            }


            if(slot.currentSecondRowObject==0 && yInput<0)
            {
                slot.currentSecondRowObject = slot.secondRowObjects.Length-1;
            }
            else if(slot.currentSecondRowObject==slot.secondRowObjects.Length-1 && yInput>0)
            {
                slot.currentSecondRowObject = 0;
            }
            else
            {
                slot.currentSecondRowObject += yInput;
            }
        }
        isRotating = false;
    }
}