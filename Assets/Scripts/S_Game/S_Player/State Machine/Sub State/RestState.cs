using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
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

        if (interactionController.canRest)
        {
            SpawnVFX();
            movement.MoveToPos(interactionController.restPos, restLerpTime);
        }
        
        lastElapsedTime = restLerpTime;
    }

    void SpawnVFX()
    {
        anim.StartRestVFX();
        CameraController.Instance.Bloom();
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
                SaveGame();
                stateMachine.EnableChangeState();
            }
        }
        
        if (player.inputManager.interactionInput)
        {
            player.inputManager.UseInteractionInput();
            stateMachine.ChangeState(player.idleState);
        }
    }

    void SaveGame()
    {
        if (DataPersistenceManager.Instance)
        {
            DataPersistenceManager.Instance.SaveGame();
        }
        else Debug.LogError("There is no data persistence object");
    }
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
