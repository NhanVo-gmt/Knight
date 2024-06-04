using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonObject<ObjectPoolManager>
{
    private ObjectPoolFactory objectPoolFactory;
    
    protected override void Awake()
    {
        base.Awake();

        objectPoolFactory = GetComponent<ObjectPoolFactory>();
    }

    public GameObject SpawnPooledPrefab(PooledObjectData data, Vector2 characterPos)
    {
        return SpawnPooledPrefab(data, characterPos, Vector2.left);
    }

    public GameObject SpawnPooledPrefab(PooledObjectData data, Vector2 characterPos, Vector2 faceDirection)
    {
        return objectPoolFactory.SpawnPooledPrefab(data, characterPos, faceDirection);
    }

    private GameObject SpawnVFXPrefab(VFXData data)
    {
        return objectPoolFactory.SpawnVFXPrefab(data);
    }
    
    private GameObject SpawnItemPrefab(ItemData data)
    {
        return objectPoolFactory.SpawnItemPrefab(data);
    }

    private GameObject SpawnParticlePrefab(ParticleData data)
    {
        return objectPoolFactory.SpawnParticlePrefab(data);
    }

    private GameObject SpawnProjectilePrefab(ProjectileData data)
    {
        return objectPoolFactory.SpawnProjectilePrefab(data);
    }

    private GameObject SpawnSpellPrefab(SpellData data)
    {
        return objectPoolFactory.SpawnSpellPrefab(data);
    }

}
