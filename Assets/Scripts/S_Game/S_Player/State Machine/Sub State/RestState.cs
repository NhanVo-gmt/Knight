using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestState : AbilityState
{
    public RestState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.inputManager.UseInteractionInput();
        Debug.Log(1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicsUpdate()
    {
        if (player.inputManager.interactionInput)
        {
            player.inputManager.UseInteractionInput();
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
