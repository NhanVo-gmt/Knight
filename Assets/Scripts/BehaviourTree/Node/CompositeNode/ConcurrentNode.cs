using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcurrentNode : CompositeNode
{
    List<State> childStateList = new List<State>();
    
    protected override void OnStart()
    {
        
    }

    void InitializeChildState()
    {
        for (int i = 0; i < children.Count; i++)
        {
            childStateList[i] = State.RUNNING;
        }
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (childStateList[i] == State.SUCCESS) continue;

            childStateList[i] = children[i].Update();
            if (childStateList[i] == State.FAILURE)
            {
                Abort();
                return State.FAILURE;
            }
        }

        if (AllChildSuccess())
        {
            return State.SUCCESS;
        }

        return State.RUNNING;
    }
    
    bool AllChildSuccess()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (childStateList[i] != State.SUCCESS) return false;
        }

        return true;
    }
}
