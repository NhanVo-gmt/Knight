using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Data/RangeAttackData", fileName = "RangeAttackData")]
public class RangeAttackData : AttackData
{
    [System.Serializable]
    public struct ProjectileData
    {
        public GameObject projectile;
        public int damage;
        public float velocity;
    }

    public ProjectileData projectileData;
    public Vector2 spawnPosition;
}
