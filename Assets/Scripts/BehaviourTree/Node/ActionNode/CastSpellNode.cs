using UnityEngine;

public class CastSpellNode : ActionNode
{
    
    public override void CopyNode(Node copyNode)
    {
        CastSpellNode node = copyNode as CastSpellNode;
        
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
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
        return NodeComponent.State.SUCCESS;
    }
    

}
