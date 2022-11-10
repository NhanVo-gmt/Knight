using UnityEngine;

public class ChasePlayerNode : ActionNode
{
    [SerializeField] float velocity;

    
    public override void CopyNode(ActionNode copyNode)
    {
        ChasePlayerNode node = copyNode as ChasePlayerNode;
        if (node != null)
        {
            velocity = node.velocity;
        }
    }
    
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
        if (!treeComponent.data.isFlying)
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
        else
        {
            Vector2 direction = (player.transform.position - treeComponent.transform.position).normalized;
            movement.SetVelocity(direction * velocity);
        }
    }


    public override void Abort()
    {
        base.Abort();

        movement.SetVelocityZero();
    }
}
