using UnityEngine;

public class PatrolNode : ActionNode
{
    [SerializeField] Vector2[] pointWays;
    [SerializeField] float velocity;

    int currentIndex = -1;
    
    
    int moveId = Animator.StringToHash("Move");


    protected override void OnStart()
    {
        base.OnStart();

        if (currentIndex == -1)
        {
            currentIndex = 0;
        }

        MoveToNextPoint();
    }

    protected override void PlayAnimation()
    {
        anim.Play(moveId);
    }

    void MoveToNextPoint()
    {
        if (treeComponent.transform.position.x < pointWays[currentIndex].x)
        {
            movement.SetVelocityX(velocity);
        }
        else
        {
            movement.SetVelocityX(-velocity);
        }
    }

    protected override void OnStop()
    {
        movement.SetVelocityZero();
        SetNextIndex();
    }

    void SetNextIndex()
    {
        currentIndex++;
        if (currentIndex >= pointWays.Length)
        {
            currentIndex = 0;
        }
    } 

    protected override State OnUpdate()
    {
        if (Mathf.Abs(Vector2.SqrMagnitude((Vector2)treeComponent.transform.position - pointWays[currentIndex])) < 0.5f)
        {
            return State.SUCCESS;
        }
        return State.RUNNING;
    }
}
