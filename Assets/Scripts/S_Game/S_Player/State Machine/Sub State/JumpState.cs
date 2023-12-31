using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Manager;
using UnityEngine;

public class JumpState : GroundState
{
    public JumpState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        particleController.SetRunParticle(false);
        UseInput();
        Jump();
        SpawnVFX();
        PlaySound();
    }

    void UseInput()
    {
        player.inputManager.UseJumpInput();
    }

    void Jump()
    {
        float force = data.jumpData.jumpForce;
        if (movement.GetVelocity().y < 0)
            force -= movement.GetVelocity().y;
        movement.AddForce(Vector2.up, force);
    }

    private void PlaySound()
    {
        SoundManager.Instance.PlayOneShot(data.jumpData.clip);
    }

    private void SpawnVFX()
    {
        vfx.SpawnPooledPrefab(data.jumpData.jumpVFX, movement);
        particleController.SetlandParticle(true);
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Move();
    }

    void Move() 
    {
        movement.SetVelocityX(player.inputManager.movementInput.x * data.moveData.moveMaxSpeed);
    }
}
