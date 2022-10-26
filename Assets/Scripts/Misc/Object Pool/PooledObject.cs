using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public IObjectPool<GameObject> pool;
    Animator anim;

    void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable() {
        Invoke("Release", 1f);
    }

    // Used in animation clip
    public void Release()
    {
        anim.Rebind();
        anim.Update(0f);

        pool.Release(gameObject);
    }
}
