using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledObject
{
    private ProjectileData projectileData;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D col;

    protected override void Awake() 
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
    

    public override void Initialize(PooledObjectData data)
    {
        base.Initialize(data);
        
        projectileData = (ProjectileData)data;
        
        projectileData.Initialize(transform, rb, anim);
        
        SetUp();
    }

    void SetUp()
    {
        sprite.sprite = projectileData.GetSprite();
        anim.runtimeAnimatorController = projectileData.GetRuntimeAnim();
        col.size = projectileData.GetCollider2D().size;
        col.offset = projectileData.GetCollider2D().offset;

        projectileData.OnBeginRelease += BeginRelease;
        projectileData.OnRelease += Release;
    }


    private void Update()
    {
        if (!projectileData) return;
        
        projectileData.Update();
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
        if (projectileData.hasExploded) return;
        
        if (other.TryGetComponent<Combat>(out Combat combat))
        {
            projectileData.OnHitTarget();
            combat.TakeDamage(projectileData.damage, IDamageable.DamagerTarget.Enemy, GetDirection());
            BeginRelease();
        }
    }

    void BeginRelease()
    {
        StartCoroutine(projectileData.ReleaseCoroutine());
    }
}
