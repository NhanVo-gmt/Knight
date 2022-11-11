using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Enemy/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public BehaviourTree tree;
    
    public HealthData healthData;

    public bool isFlying;

    public MeleeAttackData meleeAttackData;
    public RangeAttackData rangeAttackData;

}
