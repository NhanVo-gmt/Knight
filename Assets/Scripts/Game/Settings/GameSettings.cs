using UnityEngine;

public class GameSettings : SingletonObject<GameSettings>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [Header("Player")]
    public int maxHealth;

    [Header("Trap")]
    public AttackData TouchAttackSettings;
    public AttackData TrapSettings;
    
}
