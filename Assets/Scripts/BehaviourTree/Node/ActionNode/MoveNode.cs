using UnityEngine;

public class MoveNode : ActionNode
{
    public enum MoveType
    {
        Point,
        RandomInCircle,
        ToPlayer
    }
    
    public float speed;
    public bool canFly;

    public MoveType moveType = MoveType.Point;
    
    // Point
    public Vector2 movePos;

    // Random In circle
    public float radius;

    private Vector2 startPos;
    private Vector2 destination = Vector2.zero;
    private Vector2 direction;

    public override void CopyNode(ActionNode copyNode)
    {
        MoveNode copyMoveNode = copyNode as MoveNode;

        if (copyMoveNode)
        {
            speed = copyMoveNode.speed;
            moveType = copyMoveNode.moveType;
            movePos = copyMoveNode.movePos;
            radius = copyMoveNode.radius;
        }
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);

        startPos = treeComponent.transform.position;
        
        if (moveType == MoveType.Point)
        {
            if (!canFly)
            {
                movePos.y = 0;
            }
            
            destination = startPos + movePos;
            direction = (movePos - startPos).normalized;
        }
        
    }

    protected override void OnStart()
    {
        base.OnStart();
        
        if (moveType == MoveType.RandomInCircle)
        {
            destination = startPos + Random.insideUnitCircle * radius;
            direction = (destination - (Vector2)treeComponent.transform.position).normalized;
        }
        else if (moveType == MoveType.ToPlayer)
        {
            destination = treeComponent.player.transform.position;
        }
    }


    protected override NodeComponent.State OnUpdate()
    {
        if (CheckMove())
        {
            return NodeComponent.State.SUCCESS;
        }
        
        Move();
        
        return NodeComponent.State.RUNNING;
    }

    bool CheckMove()
    {
        return Vector2.Distance(destination, treeComponent.transform.position) < 0.1f;
    }

    void Move()
    {
        if (moveType == MoveType.ToPlayer)
        {
            destination = treeComponent.player.transform.position;
            direction = (destination - (Vector2)treeComponent.transform.position).normalized;
        }
        
        movement.SetVelocity(direction * speed);
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        if (moveType == MoveType.Point)
            GizmosDrawer.DrawSphere(movePos + (Vector2)selectedGameObject.transform.position, 0.5f);
        else 
            GizmosDrawer.DrawWireSphere((Vector2)selectedGameObject.transform.position, radius);
    }
}
