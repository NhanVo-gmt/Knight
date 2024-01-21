using UnityEngine;

public class MoveRandomNode : ActionNode
{
    public enum MoveRandomType
    {
        Box,
        Circle
    }

    public MoveRandomType type;
    
    //Box
    public float height;
    public float width;
    
    //Circle
    public float radius;

    public float speed;

    private Vector2 destination;
    private Vector2 startPos;
    private Vector2 direction;
    
    public override void CopyNode(Node copyNode)
    {
        MoveRandomNode node = copyNode as MoveRandomNode;

        if (node)
        {
            height = node.height;
            width = node.width;
            radius = node.radius;
            speed = node.speed;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        startPos = treeComponent.transform.position;
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        if (type == MoveRandomType.Circle)
        {
            destination = startPos + Random.insideUnitCircle * radius;
            direction = (destination - (Vector2)treeComponent.transform.position).normalized;
        }
        
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
        if (type == MoveRandomType.Circle)
        {
            GizmosDrawer.DrawWireSphere(selectedGameObject.transform.position, radius);
        }
        else if (type == MoveRandomType.Box)
        {
            GizmosDrawer.DrawWireCube(selectedGameObject.transform.position, new Vector2(width, height));
        }
    }
}
