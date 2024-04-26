using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Inventory;
using Knight.Manager;
using Knight.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

    private InteractionController interactionController;
    private Movement movement;
    private Combat combat;

    #endregion

    [Header("Data")]
    [SerializeField] PlayerData data;
    public Vector2 lastGroundPosition;
    
    private StateMachine stateMachine;
    private Core core;
    private Rigidbody2D rb;
    public InputManager inputManager {get; private set;}
    public Light2D light;

    private string initState = "IdleState";
    
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
        GetCoreComponent();

        GameManager.Instance.OnChangedGameState += GameManager_OnChangedGameState;
        SceneLoader.Instance.OnSceneBeforeLoading += SceneLoader_OnSceneBeforeLoading;
        SceneLoader.Instance.OnScenePlay += SceneLoader_OnScenePlay;
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
    
    private void SceneLoader_OnSceneBeforeLoading(object sender, EventArgs e)
    {
        DisablePlayer();
    }
    
    private void SceneLoader_OnScenePlay(object sender, EventArgs e)
    {
        EnablePlayer();
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

        if (stateMachine.currentState == restState) return;
        movement.SetGravityScale(data.jumpData.gravityScale);
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
        
        if (initState == restState.ToString())
        {
            stateMachine.Initialize(restState);
        }
        else
        {
            stateMachine.Initialize(idleState);
        }

        lastGroundPosition = transform.position;
    }

    void GetCoreComponent()
    {
        interactionController = core.GetCoreComponent<InteractionController>();
        movement = core.GetCoreComponent<Movement>();
        movement.InitializeData(data);

        combat = core.GetCoreComponent<Combat>();
        SetupCombatComponent(combat);
        SetupHealthComponent(core.GetCoreComponent<Health>());
        SetupRecoveryComponent(core.GetCoreComponent<RecoveryController>());
        SetupCollisionSenseComponent(core.GetCoreComponent<CollisionSenses>());
    }

    void SetupHealthComponent(Health health)
    {
        health.SetHealth(data.healthData);
    }

    void SetupCombatComponent(Combat combat)
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Player, IDamageable.KnockbackType.player); 
    }

    void SetupRecoveryComponent(RecoveryController recoveryController)
    {
        recoveryController.SetHitData(data.hitData);
    }
    
    void SetupCollisionSenseComponent(CollisionSenses collisionSenses)
    {
        collisionSenses.InitializeCollisionSense(.2f);
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

    #region On Enter Trigger

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
        else if (other.GetComponent<Exit>())
        {
            
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
    
    #endregion

    #region Player Methods

    private readonly float RespawnPosYOffset = 0.3f;
    private readonly float TimeBeforeFadeIn = 0.2f;

    public void DisablePlayer()
    {
        movement.SetGravityZero();
        inputManager.Toggle(false);
    }

    public void EnablePlayer()
    {
        movement.SetGravityNormal();
        inputManager.Toggle(true);
    }
    
    public void ChangePosition(Vector2 newPos)
    {
        StartCoroutine(ChangePositionCoroutine(newPos));
    }
    
    private IEnumerator ChangePositionCoroutine(Vector2 newPos)
    {
        yield return LightManager.Instance.FadeIn();
        
        newPos.y += RespawnPosYOffset;
        
        movement.SetPosition(newPos);

        yield return new WaitForSeconds(TimeBeforeFadeIn);
        yield return LightManager.Instance.FadeOut();
    }

    public void UpdateLastGroundPosition(Vector2 newGroundPosition)
    {
        lastGroundPosition = newGroundPosition;
    }

    public void ResetGroundPosition()
    {
        StartCoroutine(ResetGroundPositionCoroutine());
    }

    private IEnumerator ResetGroundPositionCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        ChangePosition(lastGroundPosition);
    }

    public void SetPlatformVelocity(Vector2 velocity)
    {
        movement.SetPlatformVelocity(velocity);
    }

    #endregion


    #region Get Methods

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

    #region Save Load

    public void LoadData(GameData gameData)
    {
        // Player movePos will be load by scene loader
        
        initState = gameData.playerState;
        if (initState == restState.ToString())
        {
            stateMachine.Initialize(restState);
        }
        else
        {
            stateMachine.Initialize(idleState);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerPos = transform.position;
    }
    

    #endregion
    
}
