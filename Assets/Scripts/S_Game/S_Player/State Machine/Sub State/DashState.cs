using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Manager;
using UnityEngine;

public class DashState : AbilityState
{

    float lastActiveTime;
    
    public DashState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        movement.SetGravityZero();
        SoundManager.Instance.PlayOneShot(data.dashData.clip);
        player.inputManager.UseDashInput();

        lastActiveTime = Time.time;

        movement.Dash();
        SpawnVFX();
        combat.DisableCollider();
    }


    private void SpawnVFX()
    {
        vfx.SpawnPooledPrefab(data.dashData.vfx, movement);
    }

    public override void Exit() 
    {
        combat.EnableCollider();
        
        base.Exit();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if (player.inputManager.movementInput.x * movement.faceDirection.x == -1)
        {
            if (collisionSenses.isGround)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else
            {
                stateMachine.ChangeState(player.inAirState);
            }
        }
        else if (player.inputManager.jumpInput && collisionSenses.isGround)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (player.inputManager.meleeAttackInput && player.meleeAttackState.CanAttack())
        {
            stateMachine.ChangeState(player.meleeAttackState);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public bool CanDash()
    {
        return Time.time > lastActiveTime + data.dashData.dashRefillTime;
    }
    
}

