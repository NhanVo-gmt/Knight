using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/AttackData", fileName = "Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Melee Data")]
    public int damage;
    public float radius;


    [Header("Project Data")]
    public GameObject projectile;
}
