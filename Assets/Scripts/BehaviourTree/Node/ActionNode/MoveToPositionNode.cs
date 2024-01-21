using UnityEngine;
using UnityEngine.Serialization;

public class MoveToPositionNode : ActionNode
{
    public enum MoveToPositionType
    {
        Local,
        World
    }

    public MoveToPositionType type;
    public bool canFly = false;
    public Vector2 movePos;
    public float speed;

    private Vector2 startPos;
    private Vector2 destination;
    private Vector2 direction;
    
    public override void CopyNode(Node copyNode)
    {
        MoveToPositionNode node = copyNode as MoveToPositionNode;
        if (node)
        {
            type = node.type;
            movePos = node.movePos;
            canFly = node.canFly;
            speed = node.speed;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        startPos = component.transform.position;
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        
        if (type == MoveToPositionType.Local)
        {
            destination = startPos + movePos;
        }
        else if (type == MoveToPositionType.World)
        {
            destination = movePos;
        }

        direction = destination - startPos;
        if (!canFly) direction.y = 0;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        Move();
        
        if (Vector2.Distance(destination, treeComponent.transform.position) < 0.1f) 
            return NodeComponent.State.SUCCESS;
        return NodeComponent.State.RUNNING;
    }

    void Move()
    {
        movement.SetVelocity(direction * speed);
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        base.DrawGizmos(selectedGameObject);
        float radius = 0.5f;
        if (type == MoveToPositionType.Local)
        {
            GizmosDrawer.DrawSphere((Vector2)selectedGameObject.transform.position + movePos, radius);
        }
        else if (type == MoveToPositionType.World)
        {
            GizmosDrawer.DrawSphere(movePos, radius);
        }
    }
}
