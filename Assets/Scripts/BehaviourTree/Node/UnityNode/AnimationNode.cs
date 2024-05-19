using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeAttribute("Unity/Animation")]
public class AnimationNode : ActionNode
{
    public string clipName;
    public int clipNameIndex = 0;

    public override void CopyNode(Node copyNode)
    {
        AnimationNode node = copyNode as AnimationNode;
        if (node)
        {
            clipName = node.clipName;
            clipNameIndex = node.clipNameIndex;
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        anim.Play(clipName);
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }

    #region Animation Event

    // protected void AddAnimationEvent() // to do
    // {
    //     anim.OnAnimationTrigger += AnimationTrigger;
    //     anim.OnAnimationFinishTrigger += AnimationFinishTrigger;
    // }

    // protected void RemoveAnimationEvent()
    // {
    //     anim.OnAnimationTrigger -= AnimationTrigger;
    //     anim.OnAnimationFinishTrigger -= AnimationFinishTrigger;
    // }

    protected virtual void AnimationTrigger(){}
    protected virtual void AnimationFinishTrigger(){}

#endregion
}
