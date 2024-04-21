using UnityEngine;

public class VFXNode : ActionNode
{
    [SerializeField] private VFXData data;
    
    public override void CopyNode(Node copyNode)
    {
        VFXNode node = copyNode as VFXNode;
        if (node)
        {
            data = node.data;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        ObjectPoolManager.Instance.SpawnPooledPrefab(data, movement.transform.position, movement.faceDirection);
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
