using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public AnimatorController anim {get; private set;}

    protected Player player;
    protected Core core;
    protected PlayerData data;
    protected StateMachine stateMachine;
    protected int animId;

    public State(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId)
    {
        this.player = player;
        this.core = core;
        this.data = data;
        this.stateMachine = stateMachine;
        this.animId = animId;

        GetCoreComponent();
    }

    void GetCoreComponent()
    {
        anim = core.GetCoreComponent<AnimatorController>();
    }

    public virtual void Enter() 
    {
        anim.Play(animId);
        anim.onAnimationFinishTrigger += AnimationFinishTrigger;
        anim.onAnimationTrigger += AnimationTrigger;
    }

    public virtual void Exit() 
    {
        anim.onAnimationFinishTrigger -= AnimationFinishTrigger;
        anim.onAnimationTrigger -= AnimationTrigger;
    }

    public virtual void PhysicsUpdate() {}
    public virtual void LogicsUpdate() {}

    public virtual void AnimationTrigger() {}
    public virtual void AnimationFinishTrigger() {}
}
