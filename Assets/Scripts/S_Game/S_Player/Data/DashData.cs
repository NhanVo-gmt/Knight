using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/DashData", fileName = "DashData")]
public class DashData : ScriptableObject
{
    [Header("Dash")]
    public float initialVelocity;
    public float cooldown;

    [Header("Sound")] public AudioClip clip;

    [Header("Vfx")]
    public PooledObjectData vfx;
}
