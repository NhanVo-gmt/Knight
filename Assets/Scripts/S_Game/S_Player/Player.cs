using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Inventory;
using Knight.Manager;
using UnityEngine;

public class Player : SingletonObject<Player>, IDataPersistence
{
    #region State
    public DashState dashState {get; private set;}
    public IdleState idleState {get; private set;}
    public InAirState inAirState {get; private set;}
    public HitState hitState {get; private set;}
    public JumpState jumpState {get; private set;}
    public MoveState moveState {get; private set;}
    public MeleeAttackState meleeAttackState {get; private set;}
    public RestState restState {get; private set;}

    #endregion

    #region Animation Clip Data

    private int dashId = Animator.StringToHash("Dash");
    private int idleId = Animator.StringToHash("Idle");
    private int inAirId = Animator.StringToHash("In Air");
    private int hitId = Animator.StringToHash("Hit");
    private int jumpId = Animator.StringToHash("Jump");
    private int moveId = Animator.StringToHash("Move");
    private int meleeAttackId = Animator.StringToHash("Melee Attack");
    private int restId = Animator.StringToHash("Rest");

    #endregion

    #region Core Component

    InteractionController interactionController;
    private Movement movement;

    #endregion

    [Header("Data")]
    [SerializeField] PlayerData data;
    StateMachine stateMachine;
    Core core;
    private Rigidbody2D rb;
    
    public InputManager inputManager {get; private set;}

    private bool isGamePaused = false;

    #region Set up
    
    protected override void Awake() 
    {
        base.Awake();

        inputManager = GetComponent<InputManager>();
        core = GetComponentInChildren<Core>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() 
    {
        CreateState();
        // stateMachine.Initialize(idleState);
        GetCoreComponent();

        GameManager.Instance.OnChangedGameState += GameManager_OnChangedGameState;
        SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
    }

    private void GameManager_OnChangedGameState(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Paused)
        {
            isGamePaused = true;
        }
        else
        {
            isGamePaused = false;
        }
    }
    
    private void SceneLoader_OnSceneLoadingCompleted(object sender, EventArgs e)
    {
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
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
        restState = new RestState(this, core, stateMachine, data, restId);
    }

    void GetCoreComponent()
    {
        interactionController = core.GetCoreComponent<InteractionController>();
        movement = core.GetCoreComponent<Movement>();
        movement.InitializeData(data);
        
        SetUpCombatComponent(core.GetCoreComponent<Combat>());
        SetUpHealthComponent(core.GetCoreComponent<Health>());
        SetUpRecoveryComponent(core.GetCoreComponent<RecoveryController>());
    }

    void SetUpHealthComponent(Health health)
    {
        health.SetHealth(data.healthData);
    }

    void SetUpCombatComponent(Combat combat)
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Player, IDamageable.KnockbackType.player); 
    }

    void SetUpRecoveryComponent(RecoveryController recoveryController)
    {
        recoveryController.SetHitData(data.hitData);
    }

    #endregion

    void Update()
    {
        if (isGamePaused) return;
        stateMachine.Update();
    }
    

    void FixedUpdate() 
    {
        stateMachine.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SetConversation(other);
        
        if (other.TryGetComponent<PickupBase>(out PickupBase pickup))
        {
            InventorySystem.Instance.AddItem(pickup.GetItem(), 1);
            pickup.Release();
        }
        else if (other.TryGetComponent<CheckPoint>(out CheckPoint checkPoint))
        {
            interactionController.EnableResting(checkPoint.GetRestPos());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        UnsetConversation(other);
        
        if (other.CompareTag("CheckPoint"))
        {
            interactionController.DisableResting();
        }
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

    public void ChangeScenePosition(Vector2 newPos)
    {
        inputManager.ResetInput();
        movement.SetVelocityZero();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = newPos;
    }

    #region Get 

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        return core.GetCoreComponent<T>();
    }

    #endregion

    #region On Draw Gizmos
    
    
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(data.meleeAttackData.leftAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
        Gizmos.DrawWireSphere(data.meleeAttackData.rightAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
    }


    #endregion

    public void LoadData(GameData data)
    {
        transform.position = data.playerPos;
        if (data.playerState == restState.ToString())
        {
            // stateMachine.Initialize(idleState);
            stateMachine.Initialize(restState);
        }
        else
        {
            stateMachine.Initialize(idleState);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerPos = transform.position;
        data.playerState = stateMachine.currentState.ToString();
    }
}
