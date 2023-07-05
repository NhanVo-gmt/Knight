using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelNode : CompositeNode
{
    enum SuccessType
    {
        OneNode,
        AllNode,
    }
    
    [SerializeField] SuccessType successType;
    NodeComponent.State[] childStateArray;
    
    protected override void OnStart()
    {
        InitializeChildState();
    }

    void InitializeChildState()
    {
        if (childStateArray == null)
        {
            childStateArray = new NodeComponent.State[children.Count];
        }

        for (int i = 0; i < childStateArray.Length; i++)
        {
            childStateArray[i] = NodeComponent.State.RUNNING;
        }

    }

    protected override void OnStop()
    {

    }

    protected override NodeComponent.State OnUpdate()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (childStateArray[i] == NodeComponent.State.SUCCESS) 
            {
                if (successType == SuccessType.OneNode)
                {
                    Abort();
                    return NodeComponent.State.SUCCESS;
                }
                continue;
            }

            childStateArray[i] = children[i].Update();
            if (childStateArray[i] == NodeComponent.State.FAILURE)
            {
                Abort();
                return NodeComponent.State.FAILURE;
            }
        }

        if (AllChildSuccess())
        {
            return NodeComponent.State.SUCCESS;
        }

        return NodeComponent.State.RUNNING;
    }
    
    bool AllChildSuccess()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (childStateArray[i] != NodeComponent.State.SUCCESS) return false;
        }

        return true;
    }
}
