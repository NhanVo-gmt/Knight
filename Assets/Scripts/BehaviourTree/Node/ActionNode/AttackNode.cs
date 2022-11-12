using UnityEngine;

public class AttackNode : ActionNode
{
    int attackId = Animator.StringToHash("Attack");
    
    public override void CopyNode(ActionNode copyNode)
    {
        AttackNode node = copyNode as AttackNode;
        if (node)
        {
            description = node.description;
        }
    }

    protected override void PlayAnimation()
    {
        anim.Play(attackId);
    }
    
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
    

}
