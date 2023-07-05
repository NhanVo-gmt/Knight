using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
{
    int currentIndex;


    protected override void OnStart()
    {
        currentIndex = 0;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
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
