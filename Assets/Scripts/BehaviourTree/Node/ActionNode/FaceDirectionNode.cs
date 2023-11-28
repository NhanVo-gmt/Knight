using UnityEngine;

public class FaceDirectionNode : ActionNode
{
    public enum Direction
    {
        Left = -1,
        Right = 1,
        Player,
    }

    public Direction direction = Direction.Left;
    
    public override void CopyNode(Node copyNode)
    {
        FaceDirectionNode node = copyNode as FaceDirectionNode;

        if (node)
        {
            direction = node.direction;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        if (direction == Direction.Player)
        {
            movement.ChangeDirection(treeComponent.player.transform.position.x - treeComponent.transform.position.x);
        }
        else
        {
            movement.ChangeDirection((int)direction);
        }
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }
    

}
