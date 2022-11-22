using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectController : CoreComponent
{
    ObjectPooling poolingManager;
    Movement movement;
    
    protected override void Awake()
    {
        base.Awake();

        poolingManager = FindObjectOfType<ObjectPooling>();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
    }

#region 

    public GameObject SpawnPooledPrefab(PooledObjectData data)
    {
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(data.pooledObject);
        SetPrefabPosition(spawnedPrefab, data);
        SetPrefabRotation(spawnedPrefab, data);

        return spawnedPrefab;
    }

    void SetPrefabPosition(GameObject spawnedObject, PooledObjectData data)
    { 
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            spawnedObject.transform.position = new Vector2(-data.pooledObjectSpawnPos.x, data.pooledObjectSpawnPos.y);
        }
        else
        {
            spawnedObject.transform.position = data.pooledObjectSpawnPos;
        }

        spawnedObject.transform.position += transform.position;
    }

    void SetPrefabRotation(GameObject spawnedObject, PooledObjectData data)
    {
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            spawnedObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            spawnedObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

#endregion
}
