using UnityEngine;

public class SeePlayerNode : ActionNode
{
    [SerializeField] float seeDistance;

    public override void CopyNode(ActionNode copyNode)
    {
        SeePlayerNode node = copyNode as SeePlayerNode;
        if (node != null)
        {
            seeDistance = node.seeDistance;
        }
    }

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (Mathf.Abs(Vector2.SqrMagnitude(treeComponent.transform.position - player.transform.position)) < seeDistance * seeDistance)
        {
            return State.SUCCESS;
        }
        return State.FAILURE;
    }
}
