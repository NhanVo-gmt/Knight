using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/HitData", fileName = "HitData")]
public class HitData : ScriptableObject
{
    [Header("Hit")]
    public float invulnerableTime;
    
    [Space(5)]
    [Range(0, 1f)] public float hitSleepTime; // Feel more impact

    [Header("VFX")] public PooledObjectData[] vfx;
    
    [Header("Sound")] public AudioClip clip;

    [Header("Cam Shake")] public CamShakeData camShakeData;
}
