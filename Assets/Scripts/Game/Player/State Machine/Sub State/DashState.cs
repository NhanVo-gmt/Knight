using System.Collections;
using System.Collections.Generic;
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

        movement.SetVelocityZero();
        movement.SetGravityZero();

        player.inputManager.UseDashInput();

        lastActiveTime = Time.time;

        movement.SetVelocityX(movement.direction.x * data.dashData.initialVelocity);
    }

    public override void Exit() 
    {
        base.Exit();
    }

    public override void AnimationFinishTrigger()
    {
        movement.SetVelocityZero();
        movement.SetGravityNormal();

        base.AnimationFinishTrigger();
    }

    public bool CanDash()
    {
        return Time.time > lastActiveTime + data.dashData.cooldown;
    }
    
}

