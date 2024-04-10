using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using Knight.Manager;
using UnityEngine;
using UnityEngine.VFX;

public class HitState : State
{
    protected Movement movement { get => _movement ??= core.GetCoreComponent<Movement>(); }
    private Movement _movement;

    public bool needToResetPlayerPosition = false;
    
    protected ParticleSystemController particleController
    {
        get => _particleController ??= core.GetCoreComponent<ParticleSystemController>(); 
    }
    private ParticleSystemController _particleController;
    
    public HitState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (needToResetPlayerPosition)
        {
            GameManager.Instance.Sleep(data.hitData.hitResetSleepTime, () => BeforeHit(), () => AfterHit());
        }
        else
        {
            GameManager.Instance.Sleep(data.hitData.hitSleepTime, () => BeforeHit(), null);
        }
    }

    void BeforeHit()
    {
        SpawnVFX();
        anim.Play(animId);
        SoundManager.Instance.PlayOneShot(data.hitData.clip);
        CameraController.Instance.Shake(data.hitData.camShakeData.shakeDuration, data.hitData.camShakeData.shakeAmount, 
            data.hitData.camShakeData.shakeFrequency);
    }
    
    void AfterHit()
    {
        player.ResetGroundPosition();
    }

    private void SpawnVFX()
    {
        anim.StartHitVFX();
        vfx.SpawnPooledPrefab(data.hitData.vfx[0], movement.transform.position, movement.faceDirection);
        particleController.PlayHitParticle();
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
