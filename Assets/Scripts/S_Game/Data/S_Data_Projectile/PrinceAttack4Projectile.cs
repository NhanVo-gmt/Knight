using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinceAttack4Projectile : ProjectileData
{
    private float explodeTimeElapse = 0f;
    private float maxExplodeTime = 3f;

    public override void Initialize(Transform transform, Rigidbody2D rb, Animator anim)
    {
        base.Initialize(transform, rb, anim);

        explodeTimeElapse = 0f;
    }

    public override void Move()
    {
        base.Move();
    }
}
