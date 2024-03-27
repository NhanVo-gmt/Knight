using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using UnityEngine;

public class GroundState : State
{
    protected Movement movement { get => _movement ??= core.GetCoreComponent<Movement>(); }
    private Movement _movement;
    
    protected InteractionController interactionController
    {
        get => _interactionController ??= core.GetCoreComponent<InteractionController>(); 
    }
    private InteractionController _interactionController;
    
    protected ParticleSystemController particleController
    {
        get => _particleController ??= core.GetCoreComponent<ParticleSystemController>(); 
    }
    private ParticleSystemController _particleController;
    
    public GroundState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.Play(animId);
        movement.SetGravityScale(data.jumpData.gravityScale);
        particleController.PlayRunParticle(true);
        
        player.dashState.RecoverDash();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
        
        LerpCamera();
        
        UpdateGroundRespawn();

        if (!stateMachine.canChangeState) return;

        if (player.inputManager.jumpInput && collisionSenses.canJump)
        {
            if (player.inputManager.movementInput.y < 0 && collisionSenses.isJumpThroughPlatform)
            {
                collisionSenses.GetJumpThroughPlatform()?.JumpThrough();
                player.inputManager.UseJumpInput();
            }
            else if (collisionSenses.canJump)
            {
                stateMachine.ChangeState(player.jumpState);
            }
        }
        else if (player.inputManager.dashInput && player.dashState.CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else if (player.inputManager.meleeAttackInput && player.meleeAttackState.CanAttack())
        {
            stateMachine.ChangeState(player.meleeAttackState);
        }
        else if (!collisionSenses.isGround && Mathf.Abs(movement.GetVelocity().y) > 0.1f)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else if (player.inputManager.interactionInput && interactionController.canRest)
        {
            stateMachine.ChangeState(player.restState);
        }
    }

    void LerpCamera()
    {
        // If we ane standing still or moving up
        if (movement.GetVelocity().y >= 0f && !CameraController.Instance.IsLerpingYDamping &&
            CameraController.Instance.LerpedFromPlayerFalling)
        {
            //reset so it can be called again
            CameraController.Instance.LerpedFromPlayerFalling = false;
            
            CameraController.Instance.LerpYDamping(false);
        }
        
        //Lerp if looking up and down
        if (player.inputManager.movementInput.y < 0)
        {
            CameraController.Instance.LookDown();
        }
        else if (player.inputManager.movementInput.y > 0)
        {
            CameraController.Instance.LookUp();
        }
        else
        {
            CameraController.Instance.LookNormal();
        }
    }

    private float checkRespawnGroundTime = 0.01f;    
    private float checkRespawnGroundTimeElapse = 0f;    
    
    void UpdateGroundRespawn()
    {
        if (checkRespawnGroundTimeElapse + checkRespawnGroundTime < Time.time)
        {
            checkRespawnGroundTimeElapse = Time.time;
            if (collisionSenses.isGroundRespawn)
            {
                player.lastGroundPosition = movement.GetPosition();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
