using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileData data;

    Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private bool isExplode;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<PooledObject>().OnInitData += Initialize;
    }
    

    private void Initialize(PooledObjectData data)
    {
        this.data = (ProjectileData)data;
        isExplode = false;
    }


    private void Update()
    {
        if (isExplode) return;
        
        Move();
    }

    private void Move()
    {
        if (!data) return;
        
        rb.velocity = -transform.right * data.velocity;
    }

    public ProjectileData GetData()
    {
        return data;
    }

    public Vector2 GetDirection()
    {
        return -transform.right;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isExplode) return;
        
        if (other.TryGetComponent<Combat>(out Combat combat))
        {
            isExplode = true;
            rb.velocity = Vector2.zero;
            combat.TakeDamage(data.damage, IDamageable.DamagerTarget.Enemy, GetDirection());
            StartCoroutine(ReleaseCoroutine());
        }
    }

    IEnumerator ReleaseCoroutine()
    {
        anim.Play("explosion");

        yield return new WaitForSeconds(.2f);
        
        GetComponent<PooledObject>().Release();
    }
}
