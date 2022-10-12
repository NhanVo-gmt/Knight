using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Combat : CoreComponent, IDamageable
{
    Collider2D col;
    IDamageable.DamagerType damagerType;
    Movement movement;
    Health health;


    Vector2 attackPosition;


    #region Set up
    
    public void SetUpDamagerType(IDamageable.DamagerType damagerType)
    {
        this.damagerType = damagerType;
    }

    protected override void Awake()
    {
        base.Awake();

        col = GetComponent<Collider2D>();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
        health = core.GetCoreComponent<Health>();

        health.onRecover += EnableCollider;
        health.onTakeDamage += DisableCollider;
        health.onDie += DisableCollider;   
    }


    void OnDisable() 
    {
        health.onRecover -= EnableCollider;
        health.onTakeDamage -= DisableCollider;
        health.onDie -= DisableCollider;
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
        if (movement.direction == Vector2.left)
        {
            attackPosition = (Vector2)transform.position + attackData.leftAttackPos;
        }
        else if (movement.direction == Vector2.right)
        {
            attackPosition = (Vector2)transform.position + attackData.rightAttackPos;
        }
    }

    void DealDamage(IDamageable damageableEntity, AttackData attackData)
    {
        damageableEntity.TakeDamage(attackData, GetDamagerType());
    }

    public IDamageable.DamagerType GetDamagerType()
    {
        return IDamageable.DamagerType.Player;
    }

    public void TakeDamage(AttackData attackData, IDamageable.DamagerType damagerType)
    {
        if (this.damagerType == damagerType) return ;
        
        health.TakeDamage(attackData);
    }

    #endregion

    #region Collider

    private void EnableCollider()
    {
        col.enabled = true;
    }

    private void DisableCollider()
    {
        col.enabled = false;
    }

    #endregion
}
