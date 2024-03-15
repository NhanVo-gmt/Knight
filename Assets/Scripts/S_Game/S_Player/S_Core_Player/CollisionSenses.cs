using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollisionSenses : CoreComponent
{
    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundCheckBox;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask jumpThroughMask;
    [SerializeField] private Transform noGroundCheck;
    
    [Header("Wall Check")]
    [SerializeField] Transform climableWallCheck;
    [SerializeField] Transform climableWallCheckUp;
    [SerializeField] float climableWallCheckDistance;
    [SerializeField] LayerMask climableMask;

    private Movement movement;

    private float coyoteTime;
    private float coyoteCounter;

    protected override void Awake()
    {
        base.Awake();
    }

    public void InitializeCollisionSense(float coyoteTime)
    {
        this.coyoteTime = coyoteTime;
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
    }


    public bool isGround
    {
        get => (Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, groundMask) || 
                Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, jumpThroughMask)) &&
               !Physics2D.OverlapBox(noGroundCheck.position, groundCheckBox, 0, groundMask);
    }
    
    public bool isJumpThroughPlatform
    {
        get => Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, jumpThroughMask) &&
               !Physics2D.OverlapBox(noGroundCheck.position, groundCheckBox, 0, jumpThroughMask);
    }

    public bool isClimableWallCheck
    {
        get => Physics2D.Raycast(climableWallCheck.position, movement.faceDirection, climableWallCheckDistance, climableMask);
    }

    public bool canJump
    {
        get => isGround || coyoteCounter >= 0f;
    }

    void Update()
    {
        if (isGround)
        {
            coyoteCounter = coyoteTime;
        }
        else if (coyoteCounter >= 0f)
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBox);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(noGroundCheck.position, groundCheckBox);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(climableWallCheck.position, Vector2.left * climableWallCheckDistance);
    }
}
