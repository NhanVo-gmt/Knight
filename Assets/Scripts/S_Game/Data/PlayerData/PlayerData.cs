using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Gravity")] 
    [HideInInspector] public float gravityStrength; // Downward force needed for desired JumpHeight and jumpTimeToApex
    [HideInInspector] public float gravityScale;

    [Space(5)] 
    public float fallGravityMult; // Mult with gravityScale when player is falling
    public float fallMaxSpeed;
    [Space(5)] 
    public float fastFallGravityMult; // Let player fall faster if they input the downward button
    public float maxFastFallSpeed;
    
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
