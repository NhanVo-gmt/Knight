using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public struct PooledPrefab
    {
        public GameObject prefab;
        public int amountToPool;
    }

    public List<PooledPrefab> pooledPrefabList = new List<PooledPrefab>();
    public List<IObjectPool<GameObject>> poolList = new List<IObjectPool<GameObject>>();

    int index = 0;
    void Awake() 
    {
        
        for (int i = 0; i < pooledPrefabList.Count; i++)
        {
            index = i;
            
            poolList.Add(new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, 
                                                false, pooledPrefabList[i].amountToPool, pooledPrefabList[i].amountToPool));
        }
    }

    GameObject CreatePooledItem()
    {
        GameObject createdGO = GameObject.Instantiate(pooledPrefabList[0].prefab);
        createdGO.AddComponent<PooledObject>().pool = poolList[0];
        return createdGO;
    }

    void OnTakeFromPool(GameObject go)
    {
        go.gameObject.SetActive(true);
    }
    
    void OnReturnToPool(GameObject go)
    {
        go.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject go) 
    {
        GameObject.Destroy(go.gameObject);
    }

    public GameObject GetObjectFromPool(GameObject go)
    {
        for (int i = 0; i < pooledPrefabList.Count; i++)
        {
            if (pooledPrefabList[i].prefab == go)
            {
                return poolList[i].Get();
            }
        }

        Debug.LogError("There is no game object on this pool");
        return null;
    }
}
