using UnityEngine;

public class CastSpellNode : ActionNode
{
    public SpellData spellData;

    private SpellParams spellParams;
    
    public override void CopyNode(Node copyNode)
    {
        CastSpellNode node = copyNode as CastSpellNode;
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);

        spellData = spellData.Clone<SpellData>();
        spellParams = new SpellParams();
        spellParams.enemyTransform = treeComponent.transform;
        spellParams.playerTransform = treeComponent.player.transform;
       
        spellData.Initialize(spellParams);
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        
        ObjectPoolManager.Instance.SpawnPooledPrefab(spellData, spellData.GetSpawnPos());
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
