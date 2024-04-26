using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    public IdleState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
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


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if (player.inputManager.movementInput.x != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        movement.Move(0f, 1f);
    }
}
