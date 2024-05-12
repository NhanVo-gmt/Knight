using UnityEngine;
using UnityEngine.Serialization;

public class TeleportNode : ActionNode
{
    public enum TeleportType
    {
        Relative,
        World,
    }

    public TeleportType type;   
    [FormerlySerializedAs("relativePos")] [SerializeField] public Vector2 position;
    private Vector2 destinationPos;
    
    public override void CopyNode(Node copyNode)
    {
        TeleportNode node = copyNode as TeleportNode;

        if (node)
        {
            position = node.position;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        destinationPos = position;
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
        switch (type)
        {
            case TeleportType.Relative:
                GizmosDrawer.DrawSphere(selectedGameObject.transform.position + (Vector3)position, .3f);
                break;
            case TeleportType.World:
                GizmosDrawer.DrawSphere(position, .3f);
                break;
        }
    }
}
