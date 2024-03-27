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

    [Header("Respawn check")] 
    [SerializeField] private Transform[] respawnChecks;
    [SerializeField] private Vector2 respawnCheckBox;
    
    [Header("Trap check")]
    [SerializeField] private Transform trapCheck;
    [SerializeField] private Vector2 trapCheckBox;
    [SerializeField] private LayerMask trapMask;
    
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

    public bool isGroundRespawn
    {
        get
        {
            return !isTrap && (IsGroundRespawn(groundMask) || IsGroundRespawn(jumpThroughMask));
        }
    }

    public bool IsGroundRespawn(int mask)
    {
        foreach (Transform respawnCheck in respawnChecks)
        {
            if (!Physics2D.OverlapBox(respawnCheck.position, respawnCheckBox, 0, mask))
            {
                return false;
            }
        }

        return true;
    }

    public bool isTrap
    {
        get
        {
            return Physics2D.OverlapBox(trapCheck.position, trapCheckBox, 0, trapMask);
        }
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

    public JumpThroughPlatform GetJumpThroughPlatform()
    {
        Collider2D col = Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, jumpThroughMask);
        if (col.TryGetComponent<JumpThroughPlatform>(out JumpThroughPlatform jumpThroughPlatform))
        {
            return jumpThroughPlatform;
        }

        return null;
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

        Gizmos.color = Color.yellow;
        foreach (Transform respawnCheck in respawnChecks)
        {
            Gizmos.DrawWireCube(respawnCheck.position, respawnCheckBox);
        }
        
        Gizmos.DrawWireCube(trapCheck.position, trapCheckBox);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(climableWallCheck.position, Vector2.left * climableWallCheckDistance);
    }
}
