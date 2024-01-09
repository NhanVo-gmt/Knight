using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : CompositeNode
{
    private int currentIndex = 0;

    public override void CopyNode(Node copyNode)
    {
        base.CopyNode(copyNode);
    }

    protected override void OnStart()
    {
        currentIndex = Random.Range(0, children.Count);
    }

    protected override void OnStop()
    {

    }

    protected override NodeComponent.State OnUpdate()
    {
        return children[currentIndex].Update();
    }
    

}
