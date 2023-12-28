using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : SingletonObject<VFXController>
{
    ObjectPooling poolingManager;
    
    protected override void Awake()
    {
        base.Awake();

        poolingManager = GetComponent<ObjectPooling>();
    }

#region 

public GameObject SpawnPooledPrefab(PooledObjectData data, Movement movement)
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
    else
    {
        spawnedPrefab = SpawnParticlePrefab(data as ParticleData);
    }
        
    SetUpSpawnPrefab(spawnedPrefab, data);
    SetPrefabPosition(spawnedPrefab, data, movement.transform.position, movement.faceDirection);
    SetPrefabRotation(spawnedPrefab, data, movement.faceDirection);
    return spawnedPrefab;
}

    public GameObject SpawnPooledPrefab(PooledObjectData data, Vector2 spawnPosition, Vector2 faceDirection)
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
        else
        {
            spawnedPrefab = SpawnParticlePrefab(data as ParticleData);
        }
        
        SetUpSpawnPrefab(spawnedPrefab, data);
        SetPrefabPosition(spawnedPrefab, data, spawnPosition, faceDirection);
        SetPrefabRotation(spawnedPrefab, data, faceDirection);
        return spawnedPrefab;
    }

    public GameObject SpawnVFXPrefab(VFXData data)
    {
        GameObject spawnedPrefab = poolingManager.GetVFXFromPool();
        SetVFXPrefab(spawnedPrefab, data);

        return spawnedPrefab;
    }
    
    public GameObject SpawnItemPrefab(ItemData data)
    {
        GameObject spawnedPrefab = poolingManager.GetItemFromPool();
        SetItemPrefab(spawnedPrefab, data);

        return spawnedPrefab;
    }

    public GameObject SpawnParticlePrefab(ParticleData data)
    {
        GameObject spawnedPrefab = poolingManager.GetParticleFromPool(data.particleSystem);

        return spawnedPrefab;
    }

    void SetUpSpawnPrefab(GameObject spawnedPrefab, PooledObjectData data)
    {
        spawnedPrefab.GetComponent<PooledObject>().Initialize(data.lifeTime);
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
    
    void SetVFXPrefab(GameObject createdGO, VFXData data)
    {
        createdGO.GetComponent<SpriteRenderer>().sprite = data.sprite;
        createdGO.GetComponent<Animator>().runtimeAnimatorController = data.anim;
    }

    void SetItemPrefab(GameObject createdGO, ItemData data)
    {
        createdGO.GetComponent<PickupBase>().Init(data);
    }

#endregion
}
