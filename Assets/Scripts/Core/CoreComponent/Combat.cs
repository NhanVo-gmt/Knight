using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Combat : CoreComponent, IDamageable
{
    Movement movement;

    Vector2 attackPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
    }

    #region Deal Damage Method

    public void MeleeAttack(PlayerAttackData attackData)
    {
        List<IDamageable> enemiesFound = FindDamageableEntityInRange(attackData).ToList();
        
        enemiesFound.ForEach(enemy => DealDamage(enemy, attackData));
    }

    
    IEnumerable<IDamageable> FindDamageableEntityInRange(PlayerAttackData attackData)
    {
        SetAttackPosition(attackData);
        
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(attackPosition, attackData.radius);
        foreach(Collider2D col in collider2DArray)
        {
            IDamageable idamageable = col.GetComponent<IDamageable>();
            if (idamageable != null)
            {
                yield return idamageable;
            }
        }
    }

    void SetAttackPosition(PlayerAttackData attackData)
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

    void DealDamage(IDamageable damageableEntity, PlayerAttackData attackData)
    {
        damageableEntity.TakeDamage(attackData);
    }

    #endregion



    #region Take Damage method

    public void TakeDamage(AttackData attackData)
    {
        
    }

    #endregion
}
