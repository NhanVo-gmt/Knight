using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : ActionNode
{
    public float idleTime;

    float time;

    public override void CopyNode(ActionNode copyNode)
    {
        IdleNode copyIdleNode = (IdleNode)copyNode;
        idleTime = copyIdleNode.idleTime;
    }

    protected override void OnStart()
    {
        base.OnStart();
        time = Time.time;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        if (Time.time - time >= idleTime) return NodeComponent.State.SUCCESS;

        return NodeComponent.State.RUNNING;
    }

}
