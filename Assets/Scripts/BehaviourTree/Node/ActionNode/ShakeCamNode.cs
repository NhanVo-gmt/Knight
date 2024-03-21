using UnityEngine;

public class ShakeCamNode : ActionNode
{
    public override void CopyNode(Node copyNode)
    {
        ShakeCamNode node = copyNode as ShakeCamNode;
        
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
