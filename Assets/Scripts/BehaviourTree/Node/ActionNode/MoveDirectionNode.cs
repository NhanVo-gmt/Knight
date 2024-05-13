using UnityEngine;

public class MoveDirectionNode : ActionNode
{
    public enum Direction
    {
        Left = -1,
        Right = 1,
        Player,
        PlayerReverse,
        CurrentDirection,
        CurrentDirectionReverse,
    }


    public Direction direction = Direction.Left;
    public float force;
    
    public override void CopyNode(Node copyNode)
    {
        MoveDirectionNode node = copyNode as MoveDirectionNode;
        if (node)
        {
            direction = node.direction;
            force = node.force;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        Vector2 directionVec = Vector2.zero;
        switch (direction)
        {
            case Direction.Left:
                directionVec = Vector2.left;
                break;
            case Direction.Right:
                directionVec = Vector2.right;
                break;
            case Direction.CurrentDirection:
                directionVec = movement.faceDirection;
                break;
            case Direction.CurrentDirectionReverse:
                directionVec = -movement.faceDirection;
                break;
            case Direction.Player:
                directionVec = movement.GetDirectionLeftRightFromPlayer(treeComponent.player.transform.position);
                break;
            case Direction.PlayerReverse:
                directionVec = - movement.GetDirectionLeftRightFromPlayer(treeComponent.player.transform.position);
                break;
            default:
                break;
        }

        movement.AddForce(directionVec, force);
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
