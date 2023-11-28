using UnityEngine;

public class DisableTouchCombatNode : ActionNode
{
    private Combat combat;
    
    public override void CopyNode(ActionNode copyNode)
    {
        DisableTouchCombatNode node = copyNode as DisableTouchCombatNode;
        
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        combat = component.core.GetCoreComponent<Combat>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        combat.DisableTouchCombat();
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
