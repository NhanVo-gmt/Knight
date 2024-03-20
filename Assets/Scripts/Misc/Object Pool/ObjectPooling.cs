using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class PooledPrefab
    {
        public enum PoolType
        {
            VFX,
            Item,
            ParticleSystem,
            Projectile,
            Spell
        }
        
        public GameObject prefab;
        public PoolType poolType;
        public int amountToPool;
        public IObjectPool<GameObject> pool;

        public void OnAwake() 
        {
            pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, false, amountToPool, amountToPool);
        }

        GameObject CreatePooledItem()
        {
            GameObject createdGO = GameObject.Instantiate(prefab);
            createdGO.GetComponent<PooledObject>().pool = pool;
            DontDestroyOnLoad(createdGO);
            return createdGO;
        }

        void OnTakeFromPool(GameObject go)
        {
            go.SetActive(true);
        }
        
        void OnReturnToPool(GameObject go)
        {
            go.SetActive(false);
        }

        void OnDestroyPoolObject(GameObject go) 
        {
            Destroy(go.gameObject);
        }
    }

    public List<PooledPrefab> pooledPrefabList = new List<PooledPrefab>();

    void Awake() 
    {
        foreach(PooledPrefab prefab in pooledPrefabList)
        {
            prefab.OnAwake();
        }
    }

    public GameObject GetVFXFromPool()
    {
        return pooledPrefabList[0].pool.Get();
    }
    
    public GameObject GetItemFromPool()
    {
        return pooledPrefabList[1].pool.Get();
    }

    public GameObject GetProjectileFromPool()
    {
        return pooledPrefabList[2].pool.Get();
    }
    
    public GameObject GetSpellFromPool()
    {
        return pooledPrefabList[3].pool.Get();
    }

    public GameObject GetParticleFromPool(GameObject prefab)
    {
        for (int i = 3; i < pooledPrefabList.Count; i++)
        {
            if (pooledPrefabList[i].prefab == prefab)
            {
                return pooledPrefabList[i].pool.Get();
            }
        }
        
        Debug.LogError("There is no game object on this pool");
        return null;
    }
}
