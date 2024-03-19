using UnityEngine;

public class TeleportNode : ActionNode
{
    [SerializeField] public Vector2 relativePos;
    private Vector2 destinationPos;
    
    public override void CopyNode(Node copyNode)
    {
        TeleportNode node = copyNode as TeleportNode;

        if (node)
        {
            relativePos = node.relativePos;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        destinationPos = relativePos;
        treeComponent.transform.position = treeComponent.transform.position - (Vector3)destinationPos * movement.GetDirectionMagnitude();
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        
        return NodeComponent.State.SUCCESS;
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        GizmosDrawer.DrawSphere(selectedGameObject.transform.position + (Vector3)relativePos, .3f);
    }
}
