using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonObject<ObjectPoolManager>
{
    public List<ObjectPooling> objectPoolingList;
    
    protected override void Awake()
    {
        base.Awake();
    }

    public PooledObject GetObjectFromPool(GameObject go)
    {
        foreach(ObjectPooling pooling in objectPoolingList)
        {
            if (pooling.prefab.gameObject == go)
            {
                return pooling.pool.Get();
            }
        }

        Debug.LogError("There is no object in pool");
        return null;
    }

}
