using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine{get; private set;}
    public MovingState MovingState{get; private set;}
    public IdleState IdleState{get; private set;}
    public TalkingState TalkingState{get; private set;}
    public DecryptingState DecryptingState{get; private set;}
    public MenuState MenuState{get; private set;}
    
    [SerializeField] 
    private PlayerData playerData;

    public GameObject PauseMenu;
    public GameObject Codex;
    public GameObject CheckingManager;
    
    public Animator Anim{get; private set;}
    public PlayerInputHandler InputHandler{get; private set;}
    public Rigidbody2D RB{get; private set;}
    public Slot Slot{get; private set;}

    public Vector2 CurrentVelocity{get; private set;} 
    public int FacingDirection{get; private set;}

    private Vector2 workspace;
    
    public BoxCollider2D MovementCollider{get; private set;}
    
    [HideInInspector] public DialogueManager dialogueManager;

    public Action<InputAction.CallbackContext> tempFunction;
    [HideInInspector] public GameObject tempObj;

    [HideInInspector]public bool triggerEntered;


    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        
        IdleState = new IdleState(this, StateMachine, playerData, "idle");
        MovingState = new MovingState(this, StateMachine, playerData, "moving");
        TalkingState = new TalkingState(this, StateMachine, playerData, "talking");
        DecryptingState = new DecryptingState(this, StateMachine, playerData, "decrypting");
        MenuState = new MenuState(this, StateMachine, playerData, "menu");
        
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Slot = GetComponent<Slot>();

        FacingDirection = CheckStartingDirection();
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private int CheckStartingDirection()
    {
        return transform.rotation.y == 0.0f ? 1 : -1;
    }
    
    private void AnimationTrigger()=>StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger()=>StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        triggerEntered = true;
        tempObj = collider.gameObject;
        if (collider.gameObject.CompareTag("NPC"))
        {
            dialogueManager = tempObj.GetComponent<DialogueManager>();
        }

        tempFunction = InputHandler.customInteractionEvent;
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        triggerEntered = false;
        tempObj = null;
        InputHandler.customInteractionEvent = tempFunction;
        dialogueManager = null;
    }
}