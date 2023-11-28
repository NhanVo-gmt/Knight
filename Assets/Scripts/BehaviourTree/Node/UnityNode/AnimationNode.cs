using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeAttribute("Unity/Animation")]
public class AnimationNode : ActionNode
{
    public string clipName;
    public int clipNameIndex = 0;

    public override void CopyNode(ActionNode copyNode)
    {
        
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
