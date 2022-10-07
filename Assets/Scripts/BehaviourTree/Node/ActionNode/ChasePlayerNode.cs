using UnityEngine;

public class ChasePlayerNode : ActionNode
{
    [SerializeField] float velocity;
    
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        ChasePlayer();
        return State.RUNNING;
    }


    void ChasePlayer()
    {
        if (player.transform.position.x > treeComponent.transform.position.x)
        {
            movement.SetVelocityX(velocity);
        }
        else
        {
            movement.SetVelocityX(-velocity);
        }
    }


    public override void Abort()
    {
        base.Abort();

        movement.SetVelocityZero();
    }
}
