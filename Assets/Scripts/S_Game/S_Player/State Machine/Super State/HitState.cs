using System.Collections;
using System.Collections.Generic;
using Knight.Manager;
using UnityEngine;
using UnityEngine.VFX;

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
        
        SpawnVFX();
        anim.Play(animId);
        GameManager.Instance.Sleep(data.hitData.hitSleepTime);
    }

    private void SpawnVFX()
    {
        anim.StartHitVFX();
        vfx.SpawnPooledPrefab(data.hitData.vfx[0], movement);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (!collisionSenses.isGround && movement.GetVelocity().y > 0.1f)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
