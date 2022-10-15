using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    protected Movement movement { get => _movement ??= core.GetCoreComponent<Movement>(); }
    private Movement _movement;
    
    public HitState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        movement.SetVelocityZero();

        anim.StartBlinking(data.hitData.invulnerableTime, data.hitData.blinkCooldown);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (!collisionSenses.isGround)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
