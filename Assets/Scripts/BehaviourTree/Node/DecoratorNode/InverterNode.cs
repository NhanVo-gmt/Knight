using UnityEngine;

public class InverterNode : DecoratorNode
{

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override NodeComponent.State OnUpdate()
    {
        NodeComponent.State state = child.Update();
        if (state == NodeComponent.State.SUCCESS)
        {
            return NodeComponent.State.FAILURE;
        }
        else if (state == NodeComponent.State.FAILURE)
        {
            return NodeComponent.State.SUCCESS;
        }
        
        return state;
    }
    

}
