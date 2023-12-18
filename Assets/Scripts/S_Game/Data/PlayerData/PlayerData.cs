using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    
    [Space(10)]
    
    [Header("Movement")]
    public MoveData moveData;
    public DashData dashData;
    public JumpData jumpData;
    
    [Space(10)]

    [Header("Attack")]
    public HealthData healthData;
    public HitData hitData;
    public MeleeAttackData meleeAttackData;
    public RangeAttackData rangeAttackData;

    private void OnValidate()
    {
        // gravityStrength = -(2 * jump)
    }
}
