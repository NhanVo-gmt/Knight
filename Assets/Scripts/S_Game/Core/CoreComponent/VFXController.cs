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
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(data.pooledObject);
        SetUpSpawnPrefab(spawnedPrefab, data);
        SetPrefabPosition(spawnedPrefab, data, movement);
        SetPrefabRotation(spawnedPrefab, data, movement);

        return spawnedPrefab;
    }

    void SetUpSpawnPrefab(GameObject spawnedPrefab, PooledObjectData data)
    {
        spawnedPrefab.GetComponent<PooledObject>().Initialize(data.lifeTime);
    }

    void SetPrefabPosition(GameObject spawnedObject, PooledObjectData data, Movement movement)
    { 
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            spawnedObject.transform.position = new Vector2(-data.spawnPos.x, data.spawnPos.y);
        }
        else
        {
            spawnedObject.transform.position = data.spawnPos;
        }

        spawnedObject.transform.position += movement.transform.position;
    }

    void SetPrefabRotation(GameObject spawnedObject, PooledObjectData data, Movement movement)
    {
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            spawnedObject.transform.rotation = Quaternion.Euler(data.spawnRot.x, data.spawnRot.y + 180, data.spawnRot.z);
        }
        else
        {
            spawnedObject.transform.rotation = Quaternion.Euler(data.spawnRot.x, data.spawnRot.y, data.spawnRot.z);
        }
    }

#endregion
}
