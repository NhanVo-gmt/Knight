using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Node : ScriptableObject
{
    public NodeComponent NodeComponent = new NodeComponent();

    [Header("Core Component")]
    public BehaviourTreeComponent treeComponent;

    public NodeComponent.State Update() 
    {
        if (!NodeComponent.started)
        {
            OnStart();
            NodeComponent.started = true;
        }

        NodeComponent.state = OnUpdate();

        if (NodeComponent.state == NodeComponent.State.SUCCESS || NodeComponent.state == NodeComponent.State.FAILURE)
        {
            OnStop();
            NodeComponent.started = false;
        }

        return NodeComponent.state;
    }

    public virtual Node Clone() 
    {
        return Instantiate(this);
    }

    public virtual void Abort()
    {
        OnStop();
        NodeComponent.started = false;
        NodeComponent.state = NodeComponent.State.FAILURE;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeComponent.State OnUpdate(); 

#region Draw Gizmos

    public virtual void DrawGizmos() 
    {

    } 

#endregion
}
