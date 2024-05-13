using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/Data/PooledObjectData/ProjectileData")]
public class ProjectileData : PooledObjectData
{
    [Header("Projectile")] 
    public GameObject projectile;
    public int damage;
    public float velocity;

    // Initialize Component
    public struct ProjectileComponent
    {
        public Transform transform;
        public Rigidbody2D rb;
        public Animator anim;
        public float startTime;
    }
    
    public ProjectileComponent component = new ProjectileComponent();
    public Action OnBeginRelease;
    public Action OnRelease;

    public bool hasExploded { get; protected set; }

    #region Get
    
    
    private Sprite sprite;
    private RuntimeAnimatorController runtimeAnim;
    private BoxCollider2D col;

    public Sprite GetSprite()
    {
        if (!sprite) sprite = projectile.GetComponent<SpriteRenderer>().sprite;
        return sprite;
    }
    
    public RuntimeAnimatorController GetRuntimeAnim()
    {
        if (!runtimeAnim) runtimeAnim = projectile.GetComponent<Animator>().runtimeAnimatorController;
        return runtimeAnim;
    }

    public BoxCollider2D GetCollider2D()
    {
        if (!col) col = projectile.GetComponent<BoxCollider2D>();
        return col;
    }

    public virtual void GetDirection()
    {
        //todo
    }

    public virtual void GetPrefabAfterExplosion()
    {
        //todo
    }

    #endregion

    #region Methods

    public virtual void Initialize(Transform transform, Rigidbody2D rb, Animator anim)
    {
        component.transform = transform;
        component.rb = rb;
        component.anim = anim;
        component.startTime = Time.time;
        hasExploded = false;
    }

    public virtual void Update()
    {
        Move();
    }
    
    protected virtual void Move()
    {
        component.rb.velocity = -component.transform.right * velocity;
    }

    public virtual void OnHitTarget()
    {
        hasExploded = true;
        component.rb.velocity = Vector2.zero;
    }

    public virtual IEnumerator ReleaseCoroutine()
    {
        component.anim.Play("explosion");

        yield return new WaitForSeconds(.2f);
        
        OnRelease?.Invoke();
    }

    #endregion
}
