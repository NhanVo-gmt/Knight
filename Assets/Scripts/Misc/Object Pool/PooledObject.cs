using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public IObjectPool<PooledObject> pool;
    Animator anim;

    void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    // Used in animation clip
    void Release()
    {
        anim.Rebind();
        anim.Update(0f);

        pool.Release(this);
    }


}
