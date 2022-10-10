public interface IDamageable
{
    public enum DamagerType
    {
        Player,
        Enemy,
        Trap
    }
    
    public DamagerType GetDamagerType();
    public void TakeDamage(AttackData attackData, DamagerType damagerType);
}
