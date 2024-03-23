using UnityEngine;

public class HealthNode : ActionNode
{
    public int toHealth = 0;

    private Health health;
    
    public override void CopyNode(Node copyNode)
    {
        HealthNode node = copyNode as HealthNode;
        if (node)
        {
            toHealth = node.toHealth;
        }    
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        health = treeComponent.core.GetCoreComponent<Health>();
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        health.TakeDamage(1000);
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
