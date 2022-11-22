using System;
using UnityEngine;

public class SpawnProjectileNode : ActionNode
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical,
        Diagonal,
        TowardsPlayer
    }

    public enum MoveEffect
    {
        None,
        Homing
    }

    protected SpawnObjectController spawnObjectController { get => _spawnObjectController ??= treeComponent.core.GetCoreComponent<SpawnObjectController>(); }
    private SpawnObjectController _spawnObjectController;
    
    [SerializeField] Projectile projectile;

    
    public override void CopyNode(ActionNode copyNode)
    {
        SpawnProjectileNode node = copyNode as SpawnProjectileNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        spawnObjectController.SpawnPooledPrefab(treeComponent.data.rangeAttackData.projectileData);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
    

}
