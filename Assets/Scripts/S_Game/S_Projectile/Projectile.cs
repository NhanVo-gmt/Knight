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

    [SerializeField] private bool isExplode;

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
        
        isExplode = false;
    }

    void SetUp()
    {
        anim.runtimeAnimatorController = projectileData.GetRuntimeAnim();
        sprite.sprite = projectileData.GetSprite();
        col.size = projectileData.GetCollider2D().size;
        col.offset = projectileData.GetCollider2D().offset;
    }


    private void Update()
    {
        if (isExplode) return;
        
        Move();
    }

    private void Move()
    {
        if (!projectileData) return;
        
        projectileData.Move();
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
