using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestState : GroundState
{
    private float restLerpTime = .2f;
    private float lastElapsedTime = 0f;
    
    public RestState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.inputManager.UseInteractionInput();
        stateMachine.DisableChangeState();
        
        movement.SetGravityScale(0f);
        movement.MoveToPos(interactionController.restPos, restLerpTime);
        lastElapsedTime = restLerpTime;
        
        DataPersistenceManager.Instance.SaveGame();
    }


    public override void Exit()
    {
        base.Exit();
        movement.SetGravityScale(data.jumpData.gravityScale);
    }

    public override void LogicsUpdate()
    {
        if (lastElapsedTime >= 0f)
        {
            lastElapsedTime -= Time.deltaTime;
            if (lastElapsedTime <= 0f)
            {
                stateMachine.EnableChangeState();
            }
        }
        
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
