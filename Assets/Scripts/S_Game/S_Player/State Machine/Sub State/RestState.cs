using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using UnityEngine;

public class RestState : GroundState
{
    private readonly float restLerpTime = .2f;
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
            movement.MoveToPos(interactionController.restPos, restLerpTime);
        }
        else
        {
            movement.SetVelocityZero();
            movement.SetGravityZero();
        }
        
        lastElapsedTime = restLerpTime;
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
                SpawnVFX();
                stateMachine.EnableChangeState();
            }
        }
        
        if (player.inputManager.interactionInput)
        {
            player.inputManager.UseInteractionInput();
            stateMachine.ChangeState(player.idleState);
        }
    }
    
    void SpawnVFX()
    {
        anim.StartRestVFX();
        particleController.PlayRestParticle();
        CameraController.Instance.Bloom();
    }

    void SaveGame()
    {
        if (DataPersistenceManager.Instance != null)
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
