using UnityEngine;

public interface IDamageable
{
    public enum DamagerTarget
    {
        Player,
        Enemy,
        Trap
    }

    public enum KnockbackType
    {
        none,
        weak,
        strong,
        player
    }
    
    public DamagerTarget GetDamagerType();
    public KnockbackType GetKnockbackType();
    public void TakeDamage(AttackData attackData, DamagerTarget damagerType, Vector2 attackDirection);
}
