using UnityEngine;
using UnityEngine.Serialization;

public class MoveNode : ActionNode
{
    public enum MovePosition
    {
        Point,
        RandomInCircle,
        ToPlayer,
        AwayFromPlayer,
    }
    
    public enum MoveType
    {
        Line,
        Continuous
    }
    
    public float speed;
    public bool canFly;
    

    public MovePosition movePosition = MovePosition.Point;
    
    // Point
    public Vector2 movePos;

    // Random In circle
    public float radius;

    public MoveType moveType = MoveType.Line;
    
    // Away From Player
    public float moveTime;

    private Vector2 startPos;
    private Vector2 destination = Vector2.zero;
    private Vector2 direction;
    private float startMoveTime = 0f;

    public override void CopyNode(ActionNode copyNode)
    {
        MoveNode copyMoveNode = copyNode as MoveNode;

        if (copyMoveNode)
        {
            speed = copyMoveNode.speed;
            movePosition = copyMoveNode.movePosition;
            movePos = copyMoveNode.movePos;
            radius = copyMoveNode.radius;
        }
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);

        startPos = treeComponent.transform.position;
        
        if (movePosition == MovePosition.Point)
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
        
        if (movePosition == MovePosition.RandomInCircle)
        {
            destination = startPos + Random.insideUnitCircle * radius;
            direction = (destination - (Vector2)treeComponent.transform.position).normalized;
        }
        else if (movePosition == MovePosition.ToPlayer)
        {
            SetDirectionToPlayer();
        }
        else if (movePosition == MovePosition.AwayFromPlayer)
        {
            SetDirectionToPlayer();
        }

        if (moveType == MoveType.Line) startMoveTime = moveTime;
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
        if (moveType == MoveType.Line)
        {
            startMoveTime -= Time.deltaTime;
            if (startMoveTime <= 0f)
            {
                return true;
            }
            
            return false;
        }
        
        return Vector2.Distance(destination, treeComponent.transform.position) < 0.1f;
    }

    void Move()
    {
        if (moveType == MoveType.Continuous)
        {
            SetDirectionToPlayer();
        }
        
        movement.SetVelocity(direction * speed);
    }

    void SetDirectionToPlayer()
    {
        destination = treeComponent.player.transform.position;
        direction = (destination - (Vector2)treeComponent.transform.position).normalized;
        if (movePosition == MovePosition.AwayFromPlayer) direction = -direction;
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        if (movePosition == MovePosition.Point)
            GizmosDrawer.DrawSphere(movePos + (Vector2)selectedGameObject.transform.position, 0.5f);
        else 
            GizmosDrawer.DrawWireSphere((Vector2)selectedGameObject.transform.position, radius);
    }
}
