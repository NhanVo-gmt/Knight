using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float duration = 1;
    float startTime;

    int idleId = Animator.StringToHash("Idle");
    
    protected override void OnStart()
    {
        base.OnStart();
        
        startTime = Time.time;
    }

    protected override void PlayAnimation()
    {
        anim.Play(idleId);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            return State.SUCCESS;
        }

        return State.RUNNING;
    }
}
