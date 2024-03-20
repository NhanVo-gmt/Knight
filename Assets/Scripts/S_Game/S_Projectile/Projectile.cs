using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledObject
{
    private ProjectileData projectileData;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [SerializeField] private bool isExplode;

    protected override void Awake() 
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    

    public override void Initialize(PooledObjectData data)
    {
        base.Initialize(data);
        
        projectileData = (ProjectileData)data;
        anim.runtimeAnimatorController = projectileData.GetRuntimeAnim();
        sprite.sprite = projectileData.GetSprite();
        
        isExplode = false;
    }


    private void Update()
    {
        if (isExplode) return;
        
        Move();
    }

    private void Move()
    {
        if (!projectileData) return;
        
        rb.velocity = -transform.right * projectileData.velocity;
    }

    public ProjectileData GetData()
    {
        return projectileData;
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
            combat.TakeDamage(projectileData.damage, IDamageable.DamagerTarget.Enemy, GetDirection());
            StartCoroutine(ReleaseCoroutine());
        }
    }

    IEnumerator ReleaseCoroutine()
    {
        anim.Play("explosion");

        yield return new WaitForSeconds(.2f);
        
        Release();
    }
}
