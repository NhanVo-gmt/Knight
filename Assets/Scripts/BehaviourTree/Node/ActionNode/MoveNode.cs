using UnityEngine;

public class MoveNode : ActionNode
{
    enum MoveType
    {
        forward,
        backward
    }

    enum Target
    {
        none,
        Player
    }

    enum StopType
    {
        none,
        frictional
    }
    
    [SerializeField] float velocity;
    [SerializeField] float moveTime;
    [SerializeField] Target target;
    [SerializeField] MoveType moveType;
    [SerializeField] StopType stopType;

    float startTime;
    Vector2 moveDirection;
    
    public override void CopyNode(ActionNode copyNode)
    {
        MoveNode node = copyNode as MoveNode;
        if (node)
        {
            description = node.description;
            velocity = node.velocity;
            moveTime = node.moveTime;
            moveType = node.moveType;
        }
    }
    
    protected override void OnStart()
    {
        startTime = 0;

        moveDirection = GetLastDirectionFromType();
    }

    Vector2 GetLastDirectionFromType()
    {
        Vector2 direction = Vector2.zero;
        if (target == Target.Player)
        {
            direction = player.transform.position - treeComponent.transform.position;
        }
        else
        {
            direction = movement.direction;
        }

        if (moveType == MoveType.backward)
        {
            direction = -direction;
        }

        return direction.normalized;
    }

    void Move() 
    {
        if (!treeComponent.data.isFlying)
        {
            movement.SetVelocityX(velocity * movement.direction.x);
        }
        else
        {
            movement.SetVelocity(velocity * moveDirection);
        }
    }

    protected override void OnStop()
    {
        movement.SetVelocityZero();
    }

    protected override State OnUpdate()
    {
        Move();
        
        startTime += Time.deltaTime;
        if (startTime >= moveTime)
        {
            return State.SUCCESS;
        }
        
        return State.RUNNING;
    }
    

}
