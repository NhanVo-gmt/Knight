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
        interactionController.OnFinishInteract += stateMachine.EnableChangeState;
    }


    public override void Exit()
    {
        base.Exit();
        
        interactionController.OnFinishInteract -= stateMachine.EnableChangeState;
    }


    public override void LogicsUpdate()
    {
        if (!stateMachine.canChangeState) return;
        
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
