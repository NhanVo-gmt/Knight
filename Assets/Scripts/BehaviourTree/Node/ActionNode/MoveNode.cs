using UnityEngine;

public class MoveNode : ActionNode
{
    enum MoveType
    {
        forward,
        backward
    }

    enum StopType
    {
        none,
        frictional
    }
    
    [SerializeField] float velocity;
    [SerializeField] float moveTime;
    [SerializeField] MoveType moveType;

    float startTime;
    
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

        Move();
    }

    void Move() 
    {
        if (!treeComponent.data.isFlying)
        {
            movement.SetVelocityX(velocity * movement.direction.x);
        }
    }

    protected override void OnStop()
    {
        movement.SetVelocityZero();
    }

    protected override State OnUpdate()
    {
        startTime += Time.deltaTime;
        if (startTime >= moveTime)
        {
            return State.SUCCESS;
        }
        
        return State.RUNNING;
    }
    

}
