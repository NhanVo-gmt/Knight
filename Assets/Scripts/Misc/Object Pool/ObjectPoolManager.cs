using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonObject<ObjectPoolManager>
{
    ObjectPooling poolingManager;
    
    protected override void Awake()
    {
        base.Awake();

        poolingManager = GetComponent<ObjectPooling>();
    }

#region Spawn Object

    public GameObject SpawnPooledPrefab(PooledObjectData data, Vector2 characterPos, Vector2 faceDirection)
    {
        GameObject spawnedPrefab = null;
        if (data is VFXData)
        {
            spawnedPrefab = SpawnVFXPrefab(data as VFXData);
        } 
        else if (data is ItemData)
        {
            spawnedPrefab = SpawnItemPrefab(data as ItemData);
        }
        else if (data is ProjectileData)
        {
            spawnedPrefab = SpawnProjectilePrefab(data as ProjectileData);
        }
        else
        {
            spawnedPrefab = SpawnParticlePrefab(data as ParticleData);
        }
        
        SetUpSpawnPrefab(spawnedPrefab, data);
        SetPrefabPosition(spawnedPrefab, data, characterPos, faceDirection);
        SetPrefabRotation(spawnedPrefab, data, faceDirection);
        return spawnedPrefab;
    }
    
    void SetUpSpawnPrefab(GameObject spawnedPrefab, PooledObjectData data)
    {
        spawnedPrefab.GetComponent<PooledObject>().Initialize(data);
    }

    void SetPrefabPosition(GameObject spawnedObject, PooledObjectData data, Vector3 position, Vector2 faceDirection)
    { 
        if (data.needPlayerDirection && faceDirection == Vector2.right)
        {
            spawnedObject.transform.position = new Vector2(-data.spawnPos.x, data.spawnPos.y);
        }
        else
        {
            spawnedObject.transform.position = data.spawnPos;
        }

        spawnedObject.transform.position += position;
    }

    void SetPrefabRotation(GameObject spawnedObject, PooledObjectData data, Vector2 faceDirection)
    {
        if (data.needPlayerDirection && faceDirection == Vector2.right)
        {
            spawnedObject.transform.rotation = Quaternion.Euler(data.spawnRot.x, data.spawnRot.y + 180, data.spawnRot.z);
        }
        else
        {
            spawnedObject.transform.rotation = Quaternion.Euler(data.spawnRot.x, data.spawnRot.y, data.spawnRot.z);
        }
    }
    
    #endregion

    #region Spawn Seperate Prefab

    private GameObject SpawnVFXPrefab(VFXData data)
    {
        GameObject spawnedPrefab = poolingManager.GetVFXFromPool();
        spawnedPrefab.GetComponent<SpriteRenderer>().sprite = data.sprite;
        spawnedPrefab.GetComponent<Animator>().runtimeAnimatorController = data.anim;

        return spawnedPrefab;
    }
    
    private GameObject SpawnItemPrefab(ItemData data)
    {
        GameObject spawnedPrefab = poolingManager.GetItemFromPool();
        spawnedPrefab.GetComponent<PickupBase>().Init(data);

        return spawnedPrefab;
    }

    private GameObject SpawnParticlePrefab(ParticleData data)
    {
        GameObject spawnedPrefab = poolingManager.GetParticleFromPool(data.particleSystem);

        return spawnedPrefab;
    }

    private GameObject SpawnProjectilePrefab(ProjectileData data)
    {
        GameObject spawnedPrefab = poolingManager.GetProjectileFromPool();
        
        return spawnedPrefab;
    }

#endregion
}
