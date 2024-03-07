using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using UnityEngine;

public class InAirState : State
{
    protected Movement movement {get => _movement ??= core.GetCoreComponent<Movement>();}
    private Movement _movement;
    
    protected ParticleSystemController particleController
    {
        get => _particleController ??= core.GetCoreComponent<ParticleSystemController>(); 
    }
    private ParticleSystemController _particleController;

    private bool isPlayed;
    private float fallSpeedYDampingChangeThreshold;
    
    public InAirState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
        fallSpeedYDampingChangeThreshold = CameraController.fallSpeedYDampingChangeThreshold;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayed = false;
        particleController.PlayRunParticle(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    
    public override void LogicsUpdate()
    {
        JumpCut();
        LerpCamera();
        ChangeState();
        PlayAnimation();
    }

    private void JumpCut()
    {
        if (player.inputManager.jumpCutInput)
        {
            player.inputManager.UseJumpCutInput();
            movement.SetGravityScale(data.jumpData.jumpCutGravityMult);
        }
    }

    void ChangeState()
    {
        if (player.inputManager.dashInput && player.dashState.CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else if (player.inputManager.jumpInput && collisionSenses.canJump)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (player.inputManager.meleeAttackInput && player.meleeAttackState.CanAttack())
        {
            stateMachine.ChangeState(player.meleeAttackState);
        }
        else if (collisionSenses.isGround && movement.GetVelocity().y < 0.1f)
        {
            stateMachine.ChangeState(player.idleState);
            SpawnLandVFX();
            particleController.PlaylandParticle(true);
        }
    }
    
    private void LerpCamera()
    {
        // If we are falling past a certain speed threshold
        if (movement.GetVelocity().y < fallSpeedYDampingChangeThreshold &&
            !CameraController.Instance.IsLerpingYDamping && !CameraController.Instance.LerpedFromPlayerFalling)
        {
            CameraController.Instance.LerpYDamping(true);
        }
    }

    void SpawnLandVFX()
    {
        vfx.SpawnPooledPrefab(data.jumpData.jumpVFX, movement.transform.position, movement.faceDirection);
    }

    void PlayAnimation()
    {
        if (isPlayed) return;

        if (movement.GetVelocity().y < 0)
        {
            anim.Play(animId);
            isPlayed = true;
        }
    }

    public override void PhysicsUpdate()
    {
        Move();
    }

    void Move() 
    {
        movement.Move(player.inputManager.movementInput.x, 1);
    }
}
