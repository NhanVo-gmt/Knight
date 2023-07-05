using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : ActionNode
{
    public float speed;
    public Vector2 movePos;

    private Vector2 direction;

    public override void CopyNode(ActionNode copyNode)
    {
        MoveNode copyMoveNode = copyNode as MoveNode;
        
        speed = copyMoveNode.speed;
        movePos = copyMoveNode.movePos;
    }

    protected override void OnStart()
    {
        base.OnStart();

        direction = (movePos - treeComponent.GetPosition()).normalized;
    }

    protected override NodeComponent.State OnUpdate()
    {
        if (Vector2.Distance(movePos, treeComponent.GetPosition()) < 0.1f) return NodeComponent.State.SUCCESS;

        treeComponent.rb.MovePosition(treeComponent.GetPosition() + direction * speed * Time.deltaTime);
        return NodeComponent.State.RUNNING;
    }

    public override void DrawGizmos()
    {
        GizmosDrawer.DrawSphere(movePos, 0.5f);
    }
}
