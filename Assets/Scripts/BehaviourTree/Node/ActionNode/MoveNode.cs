using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : ActionNode
{
    public float speed;
    public bool canFly;
    public Vector2 movePos;

    private Vector2 startPos;
    private Vector2 direction;

    public override void CopyNode(ActionNode copyNode)
    {
        MoveNode copyMoveNode = copyNode as MoveNode;
        
        speed = copyMoveNode.speed;
        movePos = copyMoveNode.movePos;
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);

        startPos = treeComponent.transform.position;
        if (!canFly)
        {
            movePos.y = 0;
            direction = movePos.normalized;
        }
        
        Debug.Log(direction);
    }


    protected override NodeComponent.State OnUpdate()
    {
        if (Vector2.Distance(movePos + startPos, treeComponent.transform.position) < 0.1f) return NodeComponent.State.SUCCESS;
        treeComponent.transform.Translate( direction * speed * Time.deltaTime);
        
        return NodeComponent.State.RUNNING;
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        GizmosDrawer.DrawSphere(movePos + (Vector2)selectedGameObject.transform.position, 0.5f);
    }
}
