using UnityEngine;

public class SelectorNode : CompositeNode
{
    [SerializeField] int currentIndex = -1;
    
    protected override void OnStart()
    {
        currentIndex = 0;
    }

    protected override void OnStop()
    {

    }

    protected override NodeComponent.State OnUpdate()
    {
        NodeComponent.State state = children[currentIndex].Update();
        
        if (state == NodeComponent.State.RUNNING)
        {
            return NodeComponent.State.RUNNING;
        }
        else if (state == NodeComponent.State.SUCCESS)
        {
            return NodeComponent.State.SUCCESS;
        }
        else
        {
            return ProceedNextChild();
        }
    }

    private NodeComponent.State ProceedNextChild()
    {
        currentIndex++;
        if (currentIndex >= children.Count)
        {
            return NodeComponent.State.FAILURE;
        }

        return NodeComponent.State.RUNNING;
    }
}
