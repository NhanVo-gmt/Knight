using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeCombat
{
    Collider2D col;
    IDamageable.DamagerTarget damagerTarget;
    private VFXController vfx;

    public MeleeCombat(Collider2D col, IDamageable.DamagerTarget damagerTarget)
    {
        this.col = col;
        this.damagerTarget = damagerTarget;
    }

    public bool MeleeAttack(MeleeAttackData attackData, Vector2 attackPos, Vector2 attackDirection)
    {
        List<IDamageable> enemiesFound = FindDamageableEntityInRange(attackData, attackPos).ToList();

        if (enemiesFound.Count > 0)
        {
            enemiesFound.ForEach(enemy => DealDamage(enemy, attackData.damage, attackDirection));
            return true;
        }

        return false;
    }

    
    IEnumerable<IDamageable> FindDamageableEntityInRange(MeleeAttackData attackData, Vector2 attackPos)
    {        
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(attackPos, attackData.radius);
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

    void DealDamage(IDamageable damageableEntity, int damage, Vector2 attackDirection)
    {
        damageableEntity.TakeDamage(damage, GetDamagerType(), attackDirection);
    }

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return damagerTarget;
    }
}
