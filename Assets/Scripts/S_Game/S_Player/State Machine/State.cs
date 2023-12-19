using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected AnimatorController anim;
    protected CollisionSenses collisionSenses;
    protected Health health;
    protected VFXController vfx;


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

    private void GetCoreComponent()
    {
        anim = core.GetCoreComponent<AnimatorController>();
        collisionSenses = core.GetCoreComponent<CollisionSenses>();
        health = core.GetCoreComponent<Health>();
        vfx = VFXController.Instance;
    }

    public virtual void Enter() 
    {
        AddEvent();
    }

    void AddEvent()
    {
        anim.onAnimationFinishTrigger += AnimationFinishTrigger;
        anim.onAnimationTrigger += AnimationTrigger;

        health.OnTakeDamage += ChangeToHitState;
    }

    void ChangeToHitState()
    {
        stateMachine.ChangeState(player.hitState);
    }

    void ChangeToDieState()
    {

    }

    public virtual void Exit() 
    {
        RemoveEvent();
    }

    void RemoveEvent()
    {
        anim.onAnimationFinishTrigger -= AnimationFinishTrigger;
        anim.onAnimationTrigger -= AnimationTrigger;

        health.OnTakeDamage -= ChangeToHitState;
    }

    public virtual void PhysicsUpdate() {}
    public virtual void LogicsUpdate() 
    {
        
    }

    public virtual void AnimationTrigger() {}
    public virtual void AnimationFinishTrigger() {}
}
