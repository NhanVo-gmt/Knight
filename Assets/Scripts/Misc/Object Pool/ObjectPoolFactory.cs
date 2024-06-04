using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolFactory : MonoBehaviour
{
    
    ObjectPooling poolingManager;
    
    void Awake()
    {
        poolingManager = GetComponent<ObjectPooling>();
    }


    public GameObject SpawnPooledPrefab(PooledObjectData data, Vector2 characterPos)
    {
        return SpawnPooledPrefab(data, characterPos, Vector2.left);
    }

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
        else if (data is SpellData)
        {
            spawnedPrefab = SpawnSpellPrefab(data as SpellData);
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

    public GameObject SpawnVFXPrefab(VFXData data)
    {
        GameObject spawnedPrefab = poolingManager.GetVFXFromPool();
        spawnedPrefab.GetComponent<SpriteRenderer>().sprite = data.sprite;
        spawnedPrefab.GetComponent<Animator>().runtimeAnimatorController = data.anim;

        return spawnedPrefab;
    }
    
    public  GameObject SpawnItemPrefab(ItemData data)
    {
        GameObject spawnedPrefab = poolingManager.GetItemFromPool();
        spawnedPrefab.GetComponent<PickupBase>().Init(data);

        return spawnedPrefab;
    }

    public  GameObject SpawnParticlePrefab(ParticleData data)
    {
        GameObject spawnedPrefab = poolingManager.GetParticleFromPool(data.particleSystem);

        return spawnedPrefab;
    }

    public  GameObject SpawnProjectilePrefab(ProjectileData data)
    {
        GameObject spawnedPrefab = poolingManager.GetProjectileFromPool();
        
        return spawnedPrefab;
    }

    public  GameObject SpawnSpellPrefab(SpellData data)
    {
        GameObject spawnedPrefab = poolingManager.GetSpellFromPool();
        
        return spawnedPrefab;
    }
}
