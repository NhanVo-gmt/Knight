using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public IObjectPool<GameObject> pool;
    protected Animator anim;
    private bool isReleased = false;

    protected virtual void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Initialize(PooledObjectData data)
    {
        isReleased = false;
        Invoke("Release", data.lifeTime);
    }


    // Used in animation clip
    public void Release()
    {
        if (isReleased) return;

        isReleased = true;
        CancelInvoke();
        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }

        pool.Release(gameObject);
    }
}
