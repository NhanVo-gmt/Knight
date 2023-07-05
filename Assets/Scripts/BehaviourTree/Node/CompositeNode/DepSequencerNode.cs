using UnityEngine;

public class DepSequencerNode : CompositeNode
{
    [SerializeField] int currentIndex = -1;


    protected override void OnStart()
    {
        currentIndex = 1;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    public override void Abort()
    {
        base.Abort();
    }

    protected override NodeComponent.State OnUpdate()
    {
        NodeComponent.State conditionState = children[0].Update();
        if (conditionState == NodeComponent.State.FAILURE)
        {
            Abort();
            return NodeComponent.State.FAILURE;
        }
        else if (conditionState == NodeComponent.State.SUCCESS)
        {
            return RunChildState();
        }


        return NodeComponent.State.SUCCESS;
    }
    
    NodeComponent.State RunChildState()
    {
        NodeComponent.State childState = children[currentIndex].Update();

        switch(childState)
        {
            case NodeComponent.State.RUNNING:
                return NodeComponent.State.RUNNING;
            case NodeComponent.State.FAILURE:
                return NodeComponent.State.FAILURE;
            case NodeComponent.State.SUCCESS:
                currentIndex++;
                break;
        }

        return currentIndex == children.Count ? NodeComponent.State.SUCCESS : NodeComponent.State.RUNNING;
    }
}
