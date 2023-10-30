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

    [FormerlySerializedAs("attackVfx")] [FormerlySerializedAs("vfx")] [Header("VFX")]
    public PooledObjectData attackVFX;
    [FormerlySerializedAs("hitVfx")] public PooledObjectData[] hitVFXs;
}
