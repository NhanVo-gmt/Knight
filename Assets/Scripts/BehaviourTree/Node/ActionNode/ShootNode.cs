using UnityEngine;

public class ShootNode : ActionNode
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Vector2 spawnPos;
    
    public override void CopyNode(Node copyNode)
    {
        ShootNode node = copyNode as ShootNode;

        if (node)
        {
            projectile = node.projectile;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        
        Instantiate(projectile, (Vector2)treeComponent.transform.position + spawnPos, Quaternion.identity);
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
