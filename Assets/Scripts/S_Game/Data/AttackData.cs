using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/AttackData", fileName = "Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Damage")]
    public int damage;
    public float coolDown;
    
    [Header("Sound")]
    public AudioClip clip;

    [Header("VFX")]
    public PooledObjectData[] hitVFX;
    public PooledObjectData attackVFX;

    [Header("Cam Shake")] 
    public float shakeDuration;
    public float shakeAmount;
    public float shakeFrequency;
    
}
