using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/Data/PooledObjectData/ProjectileData")]
public class ProjectileData : PooledObjectData
{
    [Header("Projectile")] public GameObject projectile;
    public int damage;
    public float velocity;
}
