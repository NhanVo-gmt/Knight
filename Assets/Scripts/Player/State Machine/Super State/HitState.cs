using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    float startBlinkTime;
    
    public HitState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();

        anim.StartBlinking(data.hitData.blinkCooldown);
    }

    public override void Exit()
    {
        base.Exit();

        anim.StopBlinking();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
    }

}
