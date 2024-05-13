using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Enemy/Boss/Prince/Attack4Projectile")]
public class PrinceAttack4Projectile : ProjectileData
{
    private float explodeTimeBeforeRelease = 1f;

    public override void Initialize(Transform transform, Rigidbody2D rb, Animator anim)
    {
        base.Initialize(transform, rb, anim);
    }

    public override void Update()
    {
        if (hasExploded) return;
        if (component.startTime + lifeTime - explodeTimeBeforeRelease <= Time.time)
        {
            Explode();
            return;
        }
        Move();
    }

    void Explode()
    {
        hasExploded = true;
        
        OnBeginRelease?.Invoke();
    }

    public override IEnumerator ReleaseCoroutine()
    {
        component.rb.velocity = Vector2.zero;
        component.anim.Play("explosion");
        yield return new WaitForSeconds(.4f);
        
        OnRelease.Invoke();
    }

    protected override void Move()
    {
        base.Move();
    }
}
