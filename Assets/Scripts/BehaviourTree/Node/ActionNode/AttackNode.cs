using UnityEngine;

public class AttackNode : ActionNode
{
    public enum AttackShape
    {
        Box,
        Circle
    }

    public AttackShape shape = AttackShape.Box;
    public int damage = 1;
    public Vector2 attackPos;
    
    // Box
    public Vector2 boxSize;
    
    //Circle
    public float circleRadius;
    
    public override void CopyNode(Node copyNode)
    {
        AttackNode node = copyNode as AttackNode;

        if (node)
        {
            shape = node.shape;
            damage = node.damage;
            attackPos = node.attackPos;
            boxSize = node.boxSize;
            circleRadius = node.circleRadius;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        Attack();
    }

    private void Attack()
    {
        Collider2D[] cols = new Collider2D[10];
        if (shape == AttackShape.Box)
        {
            cols = Physics2D.OverlapBoxAll((Vector2)treeComponent.transform.position + attackPos, boxSize, 0);
        }
        else if (shape == AttackShape.Circle)
        {
            cols = Physics2D.OverlapCircleAll((Vector2)treeComponent.transform.position + attackPos, circleRadius);
        }

        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(1, IDamageable.DamagerTarget.Enemy, Vector2.zero); // todo
            }
        }
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        if (shape == AttackShape.Box)
        {
            GizmosDrawer.DrawWireCube((Vector2)selectedGameObject.transform.position + attackPos, boxSize);
        }
        else if (shape == AttackShape.Circle)
        {
            GizmosDrawer.DrawWireSphere((Vector2)selectedGameObject.transform.position + attackPos, circleRadius);
        }
        
    }
}
