using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileData data;

    Rigidbody2D rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<PooledObject>().OnInitData += Initialize;
    }
    

    private void Initialize(PooledObjectData data)
    {
        this.data = (ProjectileData)data;
    }


    private void Update() {
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
        if (other.TryGetComponent<Combat>(out Combat combat))
        {
            combat.TakeDamage(data.damage, IDamageable.DamagerTarget.Enemy, GetDirection());
            GetComponent<PooledObject>().Release();
        }
    }
}
