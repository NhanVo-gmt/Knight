using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Enemy/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Character Component")]
    public HealthData healthData;

    [Header("Combat")]
    public IDamageable.KnockbackType KnockbackType = IDamageable.KnockbackType.weak;

    public bool hasShield = false;

    [Header("Drop")] public int numberOfSoulDropped;

    [Header("Sound")] 
    public AudioClip[] hitClips;

    public AudioClip GetRandomHitClip()
    {
        return hitClips[Random.Range(0, hitClips.Length)];
    }
}
