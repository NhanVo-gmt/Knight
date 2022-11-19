using UnityEngine;

public class MoveNode : ActionNode
{
    enum MoveType
    {
        none,
        forwardToPlayer,
        backwardFromPlayer
    }

    enum MoveDirection
    {
        none,
        upward,
        downward,
        leftward,
        rightward
    }

    enum StopType
    {
        none,
        frictional
    }
    
    [SerializeField] Vector2 velocity;
    [SerializeField] float moveTime;
    [SerializeField] MoveType moveType;
    [SerializeField] MoveDirection moveDirectionType;
    [SerializeField] StopType stopType;
    [SerializeField] LayerMask stopLayerMask;
    [SerializeField] bool changeFaceDirection = true;

    float startTime;
    Vector2 moveDirection;

    int moveId = Animator.StringToHash("Move");
    
    public override void CopyNode(ActionNode copyNode)
    {
        MoveNode node = copyNode as MoveNode;
        if (node)
        {
            description = node.description;
            velocity = node.velocity;
            moveTime = node.moveTime;
            moveType = node.moveType;
            stopType = node.stopType;
            changeFaceDirection = node.changeFaceDirection;
        }
    }

    protected override void PlayAnimation()
    {
        anim.Play(moveId);
    }
    
    protected override void OnStart()
    {
        startTime = 0;

        moveDirection = GetLastDirectionFromType();
    }

    Vector2 GetLastDirectionFromType()
    {
        Vector2 direction = Vector2.zero;
        
        switch(moveType)
        {
            case MoveType.forwardToPlayer:
                direction = player.transform.position - treeComponent.transform.position;
                break;
            case MoveType.backwardFromPlayer:
                direction = -(player.transform.position - treeComponent.transform.position);
                break;
            default:
                break;
        }

        direction = direction.normalized;

        switch(moveDirectionType)
        {
            case MoveDirection.upward:
                direction.y = 1;
                break;
            case MoveDirection.downward:
                direction.y = -1;
                break;
            case MoveDirection.leftward:
                direction.x = -1;
                break;
            case MoveDirection.rightward:
                direction.x = 1;
                break;
            default:
                break;
        }

        return direction;
    }

    void Move() 
    {
        if (!treeComponent.data.isFlying)
        {
            movement.SetVelocityX(velocity.x * movement.faceDirection.x, changeFaceDirection);
        }
        else
        {
            movement.SetVelocity(velocity * moveDirection, changeFaceDirection);
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
        if (startTime >= moveTime || HitLayerMask())
        {
            return State.SUCCESS;
        }
        
        return State.RUNNING;
    }
    
    bool HitLayerMask()
    {
        if (stopLayerMask == LayerMask.GetMask("Ground"))
        {
            if (collisionChecker.isGround)
            {
                return true;
            }
        }

        return false;
    }
}
