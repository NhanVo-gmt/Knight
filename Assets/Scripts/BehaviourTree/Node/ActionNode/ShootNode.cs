using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class ShootNode : ActionNode
{
    [SerializeField] private ProjectileData data;


    public override void CopyNode(Node copyNode)
    {
        ShootNode node = copyNode as ShootNode;

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

        ObjectPoolManager.Instance.SpawnPooledPrefab(data, movement.GetPosition(), movement.faceDirection);
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