using UnityEngine;

public class CheckHealthNode : ActionNode
{
    public enum Character
    {
        Enemy,
        Player
    }

    public Character character = Character.Enemy;
    [Range(1, 100)] public int healthPercent = 100;

    private Health health;
    
    public override void CopyNode(Node copyNode)
    {
        CheckHealthNode node = copyNode as CheckHealthNode;
        if (node)
        {
            character = node.character;
            healthPercent = node.healthPercent;
        }
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);

        if (character == Character.Enemy)
            health = treeComponent.core.GetCoreComponent<Health>();
        else
            health = treeComponent.player.GetComponentInChildren<Core>().GetCoreComponent<Health>();
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
        if (health.GetPercent() >= healthPercent) return NodeComponent.State.SUCCESS;
        
        return NodeComponent.State.FAILURE;
    }
    

}
