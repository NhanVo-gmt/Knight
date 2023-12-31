using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    public MeleeAttackState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {

    }

    public override void Exit()
    {
        base.Exit();

        movement.SetVelocityZero();
    }


    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Attack();
        SpawnVFX();
    }

    private void Attack()
    {
        if (combat.MeleeAttack(data.meleeAttackData))
        {
            CameraController.Instance.Shake(data.meleeAttackData.shakeDuration, data.meleeAttackData.shakeAmount, data.meleeAttackData.shakeFrequency);
            SpawnHitVFX();
        }

        movement.SetVelocityX(movement.faceDirection.x * data.meleeAttackData.moveVelocity);

        player.inputManager.UseMeleeAttackInput();
    }

    void SpawnVFX()
    {
        vfx.SpawnPooledPrefab(data.meleeAttackData.attackVFX, movement);
    }

    void SpawnHitVFX()
    {
        foreach (PooledObjectData hitVFX in data.meleeAttackData.hitVFX)
        {
            vfx.SpawnPooledPrefab(hitVFX, movement);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
