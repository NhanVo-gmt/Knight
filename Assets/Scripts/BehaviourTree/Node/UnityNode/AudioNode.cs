using UnityEngine;

[NodeAttribute("Unity/Audio")]
public class AudioNode : ActionNode
{
    public AudioClip clip;

    private AudioSource source;
    
    public override void CopyNode(Node copyNode)
    {
        AudioNode node = copyNode as AudioNode;
        if (node)
        {
            clip = node.clip;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        source = component.gameObject.GetComponent<AudioSource>();
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        source.PlayOneShot(clip);
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
