using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public IObjectPool<GameObject> pool;
    Animator anim;

    public Action<PooledObjectData> OnInitData;

    void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Initialize(PooledObjectData data)
    {
        Invoke("Release", data.lifeTime);
        
        OnInitData?.Invoke(data);
    }


    // Used in animation clip
    public void Release()
    {
        CancelInvoke();
        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }

        pool.Release(gameObject);
    }
}
