using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
    protected Movement movement { get => _movement ??= treeComponent.core.GetCoreComponent<Movement>(); }
    private Movement _movement;

    protected AnimatorController anim { get => _anim ??= treeComponent.core.GetCoreComponent<AnimatorController>(); }
    private AnimatorController _anim;

    protected override void OnStart()
    {
        PlayAnimation();
    }

    protected virtual void PlayAnimation(){}
}
