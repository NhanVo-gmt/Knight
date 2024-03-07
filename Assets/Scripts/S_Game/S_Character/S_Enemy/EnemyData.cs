using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Enemy/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Character Component")]
    public HealthData healthData;

    public IDamageable.KnockbackType KnockbackType = IDamageable.KnockbackType.weak;

    [Header("Drop")] public int numberOfSoulDropped;
}
