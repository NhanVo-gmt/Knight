using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlackBoard
{
    // Containing shared information for all node
    [Header("Speed")]
    public float moveSpeed;
    public float dashSpeed;

    [Header("Attack")] 
    public float attackDamage;
    public float waitTimeBeforeAttack;
    public Vector2 attackPos;
    public float attackRadius;

    [Header("Find Player")] 
    public Vector2 findPos;
    public float findRadius;

}
