using UnityEngine;

public class CheckPlayerNode : ActionNode
{
    public Vector2 checkPos;
    public float radius;
    
    public override void CopyNode(Node copyNode)
    {
        CheckPlayerNode node = copyNode as CheckPlayerNode;

        if (node)
        {
            checkPos = node.checkPos;
            radius = node.radius;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        if (CheckPlayer())
        {
            return NodeComponent.State.SUCCESS;
        }
        return NodeComponent.State.FAILURE;
    }

    bool CheckPlayer()
    {
        Debug.Log((Vector2)treeComponent.transform.position - checkPos * movement.faceDirection);
        return Vector2.Distance(treeComponent.player.transform.position, (Vector2)treeComponent.transform.position - checkPos * movement.faceDirection) <= radius;
    }
    
    public override void DrawGizmos(GameObject selectedGameObject)
    {
        GizmosDrawer.DrawWireSphere((Vector2)selectedGameObject.transform.position + checkPos, radius);
    }
}
