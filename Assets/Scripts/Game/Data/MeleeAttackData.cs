using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Data/MeleeAttackData", fileName = "PlayerAttackData")]
public class MeleeAttackData : AttackData
{
    public Vector2 leftAttackPos;
    public Vector2 rightAttackPos;
    public Vector2 upAttackPos;
    public Vector2 downAttackPos;

    public float radius;
}
