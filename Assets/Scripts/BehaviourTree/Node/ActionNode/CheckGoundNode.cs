using UnityEngine;
using UnityEngine.Serialization;

public class CheckGoundNode : ActionNode
{
    public LayerMask layerMask;
    public Vector2 size;
    public Vector2 relativeStartPos;

    private Vector2 startPos;
    
    public override void CopyNode(Node copyNode)
    {
        CheckGoundNode node = copyNode as CheckGoundNode;

        if (node)
        {
            layerMask = node.layerMask;
            size = node.size;
            relativeStartPos = node.relativeStartPos;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        startPos = relativeStartPos + (Vector2)treeComponent.transform.position;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        if (CheckGround())
        {
            return NodeComponent.State.SUCCESS;
        }

        return NodeComponent.State.FAILURE;
    }

    bool CheckGround()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(startPos, size, 0, layerMask);
        if (cols.Length > 0)
        {
            return true;
        }

        return false;
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        GizmosDrawer.DrawWireCube((Vector2)selectedGameObject.transform.position + relativeStartPos, size);
    }
}
