using UnityEngine;
using UnityEngine.Serialization;

public class AppearNode : ActionNode
{
    [SerializeField] private bool shouldAppear;
    
    private AnimatorController animController;

    
    public override void CopyNode(Node copyNode)
    {
        AppearNode node = copyNode as AppearNode;
        
        if (node)
        {
            shouldAppear = node.shouldAppear;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        animController = treeComponent.core.GetCoreComponent<AnimatorController>();
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        
        Toggle(shouldAppear);
    }

    void Toggle(bool isActive)
    {
        animController.ToggleSprite(isActive);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }
    

}
