using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AbilityState
{
    public AttackState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {

    }

    public override void Enter()
    {
        base.Enter();

        movement.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void AnimationTrigger()
    {
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
