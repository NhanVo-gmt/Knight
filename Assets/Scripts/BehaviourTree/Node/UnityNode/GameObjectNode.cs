using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeAttribute("Unity/GameObject")]
public class GameObjectNode : ActionNode
{
    public bool isActive = true;
    
    protected override void OnStart()
    {
        base.OnStart();
        SetActive();
    }

    void SetActive()
    {
        treeComponent.gameObject.SetActive(isActive);
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }
}
