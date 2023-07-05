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

        direction = (direction - treeComponent.GetPosition()).normalized;
    }

    protected override NodeComponent.State OnUpdate()
    {
        treeComponent.rb.MovePosition(treeComponent.GetPosition() + direction * speed * Time.deltaTime);
        return NodeComponent.State.SUCCESS;
    }
}
