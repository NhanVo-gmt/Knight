using UnityEngine;

public class CombatColliderNode : ActionNode
{
    [SerializeField] private bool turnCollider;
    
    private Combat combat;
    
    public override void CopyNode(Node copyNode)
    {
        CombatColliderNode node = copyNode as CombatColliderNode;

        if (node)
        {
            turnCollider = node.turnCollider;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        combat = treeComponent.core.GetCoreComponent<Combat>();
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        Toggle(turnCollider);
    }

    void Toggle(bool isActive)
    {
        if (isActive) combat.EnableCollider();
        else combat.DisableCollider();
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
