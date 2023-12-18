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
        Jump();
    }

    void Jump() 
    {
        player.inputManager.UseJumpInput();
        movement.SetVelocityY(data.jumpData.velocity);

        SpawnVFX();
        PlaySound();
    }

    private void PlaySound()
    {
        SoundManager.Instance.PlayOneShot(data.jumpData.clip);
    }

    private void SpawnVFX()
    {
        vfx.SpawnPooledPrefab(data.jumpData.jumpVFX, movement);
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
