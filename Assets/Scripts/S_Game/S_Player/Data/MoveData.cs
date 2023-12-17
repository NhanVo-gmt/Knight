using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/MoveData", fileName = "MoveData")]
public class MoveData : ScriptableObject
{
    [Header("Move")]
    public float velocity;

    [Header("Sound")] public AudioClip clip;
}
