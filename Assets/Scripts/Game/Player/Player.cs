using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State

    public DashState dashState {get; private set;}
    public IdleState idleState {get; private set;}
    public InAirState inAirState {get; private set;}
    public HitState hitState {get; private set;}
    public JumpState jumpState {get; private set;}
    public MoveState moveState {get; private set;}
    public MeleeAttackState meleeAttackState {get; private set;}

    #endregion

    #region Animation Clip Data

    int dashId = Animator.StringToHash("Dash");
    int idleId = Animator.StringToHash("Idle");
    int inAirId = Animator.StringToHash("In Air");
    int hitId = Animator.StringToHash("Hit");
    int jumpId = Animator.StringToHash("Jump");
    int meleeAttackId = Animator.StringToHash("Melee Attack");
    int moveId = Animator.StringToHash("Move");

    #endregion

    #region Core Component

    Health health;
    Movement movement;
    Combat combat;
    InteractionController interactionController;

    #endregion

    [SerializeField] PlayerData data;
    StateMachine stateMachine;
    Core core;
    public InputManager inputManager {get; private set;}

    #region Set up
    
    void Awake() 
    {
        inputManager = GetComponent<InputManager>();
        core = GetComponentInChildren<Core>();
    }

    void Start() 
    {
        CreateState();
        
        stateMachine.Initialize(idleState);

        GetCoreComponent();
    }

    void CreateState()
    {
        stateMachine = new StateMachine();
        
        dashState = new DashState(this, core, stateMachine, data, dashId);
        idleState = new IdleState(this, core, stateMachine, data, idleId);
        inAirState = new InAirState(this, core, stateMachine, data, inAirId);
        hitState = new HitState(this, core, stateMachine, data, hitId);
        jumpState = new JumpState(this, core, stateMachine, data, jumpId);
        moveState = new MoveState(this, core, stateMachine, data, moveId);
        meleeAttackState = new MeleeAttackState(this, core, stateMachine, data, meleeAttackId);
    }

    void GetCoreComponent()
    {
        movement = core.GetCoreComponent<Movement>();
        interactionController = core.GetCoreComponent<InteractionController>();
        combat = core.GetCoreComponent<Combat>();
        health = core.GetCoreComponent<Health>();

        SetUpComponent();
    }

    void SetUpComponent()
    {
        combat.SetUpDamagerType(IDamageable.DamagerType.Player);
        health.SetHealth(data.healthData);
        health.SetHitData(data.hitData);
    }

    #endregion

    void Update() 
    {
        stateMachine.Update();
    }

    void FixedUpdate() 
    {
        stateMachine.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SetConversation(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        UnsetConversation(other);
    }

    private void SetConversation(Collider2D other)
    {
        DialogueHolder npcDialogue = other.GetComponent<DialogueHolder>();
        if (npcDialogue != null)
        {
            interactionController.SetDialogue(npcDialogue.GetDialogue());
        }
    }

    private void UnsetConversation(Collider2D other)
    {
        DialogueHolder npcDialogue = other.GetComponent<DialogueHolder>();
        if (npcDialogue != null)
        {
            interactionController.UnsetDialogue(npcDialogue.GetDialogue());
        }
    }


    #region Get 

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        return core.GetCoreComponent<T>();
    }

    #endregion

    #region On Draw Gizmos
    
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(data.meleeAttackData.leftAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
        Gizmos.DrawWireSphere(data.meleeAttackData.rightAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
    }


    #endregion
}
