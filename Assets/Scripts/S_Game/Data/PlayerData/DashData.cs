using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/DashData", fileName = "DashData")]
public class DashData : ScriptableObject
{
    [Header("Dash")]
    public float dashAmount;
    public float dashSpeed;
    [Space(5)] 
    public float dashAttackTime;
    [Space(5)] 
    public float dashEndTime; // Time after the initial drag, smoothing back to idle
    public Vector2 dashEndSpeed; // Slows down player, makes the dash feels responsize

    [Range(0f, 1f)] public float dashEndRunLerp; //Slows the affect of player movement while dashing
    [Space(5)]
    public float dashRefillTime;
    [Space(5)] 
    [Range(0.01f, 0.5f)] public float dashInputBufferTime;

    [Header("Sound")] public AudioClip clip;

    [Header("Vfx")]
    public PooledObjectData vfx;
}
