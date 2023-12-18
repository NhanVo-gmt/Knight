using System.Collections;
using System.Collections.Generic;
using Knight.Manager;
using UnityEngine;

public class MoveState : GroundState
{
    private float soundPlayInterval = 0.4f;
    private float lastSoundPlayTime;
    
    public MoveState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lastSoundPlayTime = 0f;
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
        
        PlaySound();

        if (player.inputManager.movementInput.magnitude == 0)
        {
            movement.SetVelocityZero();
            
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void PlaySound()
    {
        lastSoundPlayTime -= Time.deltaTime;
        if (lastSoundPlayTime <= 0f)
        {
            lastSoundPlayTime = soundPlayInterval;
            SoundManager.Instance.PlayOneShot(data.moveData.clip);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        Move();
    }

    void Move() 
    {
        movement.Run(player.inputManager.movementInput.x, 1);
    }
    
}
