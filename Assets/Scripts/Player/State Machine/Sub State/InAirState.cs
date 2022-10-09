using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : State
{
    protected CollisionSenses collisionSenses {get => _collisionSenses ??= core.GetCoreComponent<CollisionSenses>();}
    private CollisionSenses _collisionSenses;

    protected Movement movement {get => _movement ??= core.GetCoreComponent<Movement>();}
    private Movement _movement;

    private bool isPlayed;
    
    public InAirState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {

    }

    public override void Enter()
    {
        isPlayed = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    
    public override void LogicsUpdate()
    {
        ChangeState();
        PlayAnimation();
    }

    void ChangeState()
    {
        if (player.inputManager.meleeAttackInput && player.meleeAttackState.CanAttack())
        {
            stateMachine.ChangeState(player.meleeAttackState);
        }
        else if (collisionSenses.isGround && movement.GetVelocity().y < 0.1f)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    void PlayAnimation()
    {
        if (isPlayed) return;

        if (movement.GetVelocity().y < 0)
        {
            anim.Play(animId);
            isPlayed = true;
        }
    }

    public override void PhysicsUpdate()
    {
        Move();
    }

    void Move() 
    {
        movement.SetVelocityX(player.inputManager.movementInput.x * data.moveData.velocity);
    }
}
