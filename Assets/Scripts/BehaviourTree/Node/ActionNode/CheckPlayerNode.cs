using UnityEngine;

public class CheckPlayerNode : ActionNode
{
    public float radius;

    private float lastCheckTime = 0f;
    
    
    public override void CopyNode(ActionNode copyNode)
    {
        CheckPlayerNode node = copyNode as CheckPlayerNode;

        if (node)
        {
            
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
        return Vector2.Distance(treeComponent.player.transform.position, treeComponent.transform.position) <= radius;
    }
    
    public override void DrawGizmos(GameObject selectedGameObject)
    {
        GizmosDrawer.DrawWireSphere((Vector2)selectedGameObject.transform.position, radius);
    }
}
