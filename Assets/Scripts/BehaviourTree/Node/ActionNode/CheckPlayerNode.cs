using UnityEngine;
using UnityEngine.Serialization;

public class CheckPlayerNode : ActionNode
{
    public enum CheckType
    {
        Circle,
        Box
    }

    public CheckType checkType = CheckType.Circle;
    
    public Vector2 checkRelativePos;
    public float radius;

    public Vector2 size;

    private Vector2 playerPos;
    private Vector2 checkPos;
    
    public override void CopyNode(Node copyNode)
    {
        CheckPlayerNode node = copyNode as CheckPlayerNode;

        if (node)
        {
            checkType = node.checkType;
            checkRelativePos = node.checkRelativePos;
            radius = node.radius;
            size = node.size;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        playerPos = treeComponent.player.transform.position;
        checkPos = movement.GetWorldPosFromRelativePos(checkRelativePos);
        
        Debug.Log(checkPos);
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
        if (checkType == CheckType.Circle)
        {
            return Vector2.Distance(playerPos, checkPos) <= radius;
        }
        else
        {
            Debug.Log(Mathf.Abs(playerPos.x - checkPos.x) < size.x / 2);
            Debug.Log(Mathf.Abs(playerPos.y - checkPos.y) < size.y / 2);
            return (Mathf.Abs(playerPos.x - checkPos.x) < size.x / 2 && Mathf.Abs(playerPos.y - checkPos.y) < size.y / 2);
        }
    }
    
    public override void DrawGizmos(GameObject selectedGameObject)
    {
        if (checkType == CheckType.Circle)
        {
            GizmosDrawer.DrawWireSphere((Vector2)selectedGameObject.transform.position + checkRelativePos, radius);
        }
        else
        {
            GizmosDrawer.DrawWireCube((Vector2)selectedGameObject.transform.position + checkRelativePos, size);
        }
    }
}
