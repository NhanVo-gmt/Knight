using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Enemy/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public HealthData healthData;

    public AttackData touchAttackData;
    public MeleeAttackData meleeAttackData;
    public RangeAttackData rangeAttackData;
}
