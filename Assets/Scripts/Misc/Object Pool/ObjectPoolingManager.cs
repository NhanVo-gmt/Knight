using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : SingletonObject<ObjectPoolingManager>
{
    public List<ObjectPooling> objectPoolingList = new List<ObjectPooling>();

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
