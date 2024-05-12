using UnityEngine;
using UnityEngine.Serialization;

public class CheckPlayerNode : ActionNode
{
    public enum CheckType
    {
        Circle,
        Box
    }

    public enum CheckPositionType
    {
        Relative,
        World
    }

    public CheckType checkType = CheckType.Circle;
    public CheckPositionType checkPosType = CheckPositionType.Relative;
    
    public Vector2 checkRelativePos;
    public Vector2 checkPos;
    public float radius;

    public Vector2 size;

    private Vector2 playerPos;
    
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

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }

    protected override void OnStart()
    {
        base.OnStart();

        
        playerPos = treeComponent.player.transform.position;
        if (checkPosType == CheckPositionType.Relative)
            checkPos = movement.GetWorldPosFromRelativePos(checkRelativePos);
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
            return (Mathf.Abs(playerPos.x - checkPos.x) < size.x / 2 && Mathf.Abs(playerPos.y - checkPos.y) < size.y / 2);
        }
    }
    
    public override void DrawGizmos(GameObject selectedGameObject)
    {
        GizmosDrawer.color = Color.red;
        switch (checkPosType)
        {
            case CheckPositionType.Relative:
                if (checkType == CheckType.Circle)
                {
                    GizmosDrawer.DrawWireSphere((Vector2)selectedGameObject.transform.position + checkRelativePos, radius);
                }
                else
                {
                    GizmosDrawer.DrawWireCube((Vector2)selectedGameObject.transform.position + checkRelativePos, size);
                }

                break;
            
            case CheckPositionType.World:
                if (checkType == CheckType.Circle)
                {
                    GizmosDrawer.DrawWireSphere(checkPos, radius);
                }
                else
                {
                    GizmosDrawer.DrawWireCube(checkPos, size);
                }

                break;
        }
        
    }
}
