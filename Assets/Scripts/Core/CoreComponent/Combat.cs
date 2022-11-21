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

    Movement movement;
    Health health;
    GameSettings settings;


    Vector2 attackPosition;
    Vector2 hitDirection;


    #region Set up
    
    public void SetUpCombatComponent(IDamageable.DamagerTarget damagerTarget, IDamageable.KnockbackType knockbackType)
    {
        this.damagerTarget = damagerTarget;
        this.knockbackType = knockbackType;
    }

    protected override void Awake()
    {
        base.Awake();

        col = GetComponent<Collider2D>();
    }

    void Start() 
    {
        settings = FindObjectOfType<GameSettings>();
        
        movement = core.GetCoreComponent<Movement>();
        health = core.GetCoreComponent<Health>();
        AddEvent();
    }

    void AddEvent()
    {
        health.onTakeDamage += Knockback;
    }

    private void OnDisable() {
        RemoveEvent();
    }

    private void RemoveEvent()
    {
        health.onTakeDamage -= Knockback;
    }

    #endregion

    #region Damage Method

    public void MeleeAttack(MeleeAttackData attackData)
    {
        List<IDamageable> enemiesFound = FindDamageableEntityInRange(attackData).ToList();
        
        enemiesFound.ForEach(enemy => DealDamage(enemy, attackData));
    }

    
    IEnumerable<IDamageable> FindDamageableEntityInRange(MeleeAttackData attackData)
    {
        SetAttackPosition(attackData);
        
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(attackPosition, attackData.radius);
        foreach(Collider2D col in collider2DArray)
        {
            if (col == this.col) continue;

            
            IDamageable idamageable = col.GetComponent<IDamageable>();
            if (idamageable != null)
            {
                yield return idamageable;
            }
        }
    }

    void SetAttackPosition(MeleeAttackData attackData)
    {
        if (movement.faceDirection == Vector2.left)
        {
            attackPosition = (Vector2)transform.position + attackData.leftAttackPos;
        }
        else if (movement.faceDirection == Vector2.right)
        {
            attackPosition = (Vector2)transform.position + attackData.rightAttackPos;
        }
    }

    void DealDamage(IDamageable damageableEntity, AttackData attackData)
    {
        damageableEntity.TakeDamage(attackData, GetDamagerType(), movement.faceDirection);
    }

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return damagerTarget;
    }

    public void TakeDamage(AttackData attackData, IDamageable.DamagerTarget damagerType, Vector2 attackDirection)
    {
        if (this.damagerTarget == damagerType) return ;
        
        if (attackDirection == Vector2.zero)
        {
            hitDirection = -movement.faceDirection;
        }
        else
        {
            hitDirection = attackDirection;
        }

        health.TakeDamage(attackData);
    }

    #endregion

    #region Knockback

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return knockbackType;
    }

    void Knockback()
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

    

    #endregion
}
