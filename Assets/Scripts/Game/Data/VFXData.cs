using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VFXData", menuName = "ScriptableObjects/Data/VFXData")]
public class VFXData : ScriptableObject
{
    [Header("VFX")]
    public GameObject vfx;
    public bool needPlayerDirection;
    public Vector2 vfxSpawnPos;
}
