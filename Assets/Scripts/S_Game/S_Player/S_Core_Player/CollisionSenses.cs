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
    [SerializeField] private Transform noGroundCheck;
    
    [Header("Wall Check")]
    [SerializeField] Transform climableWallCheck;
    [SerializeField] Transform climableWallCheckUp;
    [SerializeField] float climableWallCheckDistance;
    [SerializeField] LayerMask climableMask;

    private Movement movement;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
    }


    public bool isGround
    {
        get => Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, groundMask) &&
               !Physics2D.OverlapBox(noGroundCheck.position, groundCheckBox, 0, groundMask);
    }

    public bool isClimableWallCheck
    {
        get => Physics2D.Raycast(climableWallCheck.position, movement.faceDirection, climableWallCheckDistance, climableMask);
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
