using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class ObjectPooling
{
    public PooledObject prefab;
    public int amountToPool;
    public IObjectPool<PooledObject> pool;

    void Awake() 
    {
        pool = new ObjectPool<PooledObject>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, 
                                            false, amountToPool, amountToPool);
    }

    PooledObject CreatePooledItem()
    {
        PooledObject createdGO = GameObject.Instantiate(prefab);
        createdGO.pool = pool;
        return createdGO;
    }

    void OnTakeFromPool(PooledObject go)
    {
        go.gameObject.SetActive(true);
    }
    
    void OnReturnToPool(PooledObject go)
    {
        go.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(PooledObject go) 
    {
        GameObject.Destroy(go.gameObject);
    }
}
