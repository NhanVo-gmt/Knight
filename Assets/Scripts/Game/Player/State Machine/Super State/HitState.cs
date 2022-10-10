using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    float hitTime;
    
    public HitState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();

        hitTime = Time.time;

        anim.StartBlinking(data.hitData.blinkCooldown, data.hitData.invulnerableTime);
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
