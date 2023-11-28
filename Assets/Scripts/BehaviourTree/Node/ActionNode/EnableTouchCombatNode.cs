using UnityEngine;

public class EnableTouchCombatNode : ActionNode
{
    private Combat combat;
    
    public override void CopyNode(ActionNode copyNode)
    {
        EnableTouchCombatNode node = copyNode as EnableTouchCombatNode;
        
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        combat = component.core.GetCoreComponent<Combat>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        combat.EnableTouchCombat();
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
