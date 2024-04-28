using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Combat : CoreComponent, IDamageable
{
    Collider2D col;
    IDamageable.DamagerTarget damagerTarget;
    IDamageable.KnockbackType knockbackType;

    Movement movement {get => _movement ??= core.GetCoreComponent<Movement>(); }
    Movement _movement;

    Health health {get => _heath ??= core.GetCoreComponent<Health>(); }
    Health _heath;

    GameSettings settings;
    MeleeCombat meleeCombat;
    TouchCombat touchCombat;
    private ShieldCombat shieldCombat;

    private bool canTouchCombat = true;

    Vector2 attackPosition;
    Vector2 hitDirection;


    #region Set up
    
    public void SetUpCombatComponent(IDamageable.DamagerTarget damagerTarget, IDamageable.KnockbackType knockbackType, bool hasShield = false)
    {
        this.damagerTarget = damagerTarget;
        this.knockbackType = knockbackType;
        
        meleeCombat = new MeleeCombat(col, damagerTarget);
        shieldCombat = new ShieldCombat(movement, hasShield);

        if (damagerTarget == IDamageable.DamagerTarget.Player) return ;
        touchCombat = new TouchCombat(col, damagerTarget);
    }

    protected override void Awake()
    {
        base.Awake();

        col = GetComponent<Collider2D>();
    }

    void Start() 
    {
        settings = FindObjectOfType<GameSettings>();
    }



    #endregion

    #region Damage Method

    public bool MeleeAttack(MeleeAttackData attackData)
    {
        return meleeCombat.MeleeAttack(attackData, SetAttackPosition(attackData), movement.faceDirection);
    }

    Vector2 SetAttackPosition(MeleeAttackData attackData)
    {
        if (movement.faceDirection == Vector2.left)
        {
            attackPosition = (Vector2)transform.position + attackData.leftAttackPos;
        }
        else if (movement.faceDirection == Vector2.right)
        {
            attackPosition = (Vector2)transform.position + attackData.rightAttackPos;
        }

        return attackPosition;
    }

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return damagerTarget;
    }
    
    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection)
    {
        TakeDamage(damage, damagerType, attackDirection, false);
    }

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection, bool needResetPlayerPosition)
    {
        if (this.damagerTarget == damagerType) return;
        if (shieldCombat.CanShield(attackDirection)) return;
        if (!health.TakeDamage(damage, needResetPlayerPosition)) return;
        if (health.isDie) return;
        if (needResetPlayerPosition) return;

        if (attackDirection == Vector2.zero)
        {
            hitDirection = -movement.faceDirection;
        }
        else
        {
            hitDirection = attackDirection;
        }
        
        Knockback(hitDirection);
    }

    #endregion

    #region Knockback

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return knockbackType;
    }

    void Knockback(Vector2 hitDirection)
    {
        float knockbackAmount = 0;
        switch(knockbackType)
        {
            case IDamageable.KnockbackType.weak:
                knockbackAmount = settings.WeakKnockbackAmount;
                break;
            case IDamageable.KnockbackType.strong:
                knockbackAmount = settings.StrongKnockbackAmount;
                break;
            case IDamageable.KnockbackType.player:
                knockbackAmount = settings.PlayerKnockbackAmount;
                break;
            case IDamageable.KnockbackType.none:
                return;
        }
        
        movement.AddForce(hitDirection, knockbackAmount);
    }

    #endregion

    #region Collider

    public void EnableCollider()
    {
        col.enabled = true;
    }

    public void DisableCollider()
    {
        col.enabled = false;
    }

    public void EnableTouchCombat()
    {
        canTouchCombat = true;
    }

    public void DisableTouchCombat()
    {
        canTouchCombat = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTouchCombat)
        {
            touchCombat?.TouchAttack(other);
        }
    }

    #endregion
}
