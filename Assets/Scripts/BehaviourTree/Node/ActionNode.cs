using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node //to do
{
    // protected Movement movement { get => _movement ??= treeComponent.core.GetCoreComponent<Movement>(); }
    // private Movement _movement;

    // protected AnimatorController anim { get => _anim ??= treeComponent.core.GetCoreComponent<AnimatorController>(); }
    // private AnimatorController _anim;

    // protected CollisionChecker collisionChecker { get => _collisionChecker ??= treeComponent.core.GetCoreComponent<CollisionChecker>(); }
    // private CollisionChecker _collisionChecker;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    public abstract void CopyNode(ActionNode copyNode);


}
