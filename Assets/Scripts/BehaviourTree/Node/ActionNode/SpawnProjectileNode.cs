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
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
    

}
