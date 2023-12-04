using UnityEngine;

public class CheckEnemyHealth : ActionNode
{
    [Range(1, 100)] public int healthPercent = 90;

    private Health health;
    
    public override void CopyNode(Node copyNode)
    {
        CheckEnemyHealth node = copyNode as CheckEnemyHealth;
        
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);

        health = treeComponent.core.GetCoreComponent<Health>();
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        if (health.GetPercent() >= healthPercent) return NodeComponent.State.SUCCESS;
        
        return NodeComponent.State.FAILURE;
    }
    

}
