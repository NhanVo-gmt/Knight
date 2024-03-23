using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeAttribute("Unity/Debug")]
public class DebugNode : ActionNode
{
    public enum DebugType
    {
        Log,
        LogWarning,
        LogError
    }

    public DebugType type;
    public string text;

    public override void CopyNode(Node copyNode)
    {
        DebugNode node = copyNode as DebugNode;
        if (node)
        {
            type = node.type;
            text = node.text;
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        CallDebug();
    }

    void CallDebug()
    {
        switch (type)
        {
            case DebugType.Log:
                Debug.Log(text);
                return;
            case DebugType.LogWarning:
                Debug.LogWarning(text);
                return;
            case DebugType.LogError:
                Debug.LogError(text);
                return;
        }
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }

    #region Animation Event

    // protected void AddAnimationEvent() // to do
    // {
    //     anim.onAnimationTrigger += AnimationTrigger;
    //     anim.onAnimationFinishTrigger += AnimationFinishTrigger;
    // }

    // protected void RemoveAnimationEvent()
    // {
    //     anim.onAnimationTrigger -= AnimationTrigger;
    //     anim.onAnimationFinishTrigger -= AnimationFinishTrigger;
    // }

    protected virtual void AnimationTrigger(){}
    protected virtual void AnimationFinishTrigger(){}

#endregion
}
