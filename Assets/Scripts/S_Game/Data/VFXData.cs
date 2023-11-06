using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VFXData", menuName = "ScriptableObjects/Data/PooledObjectData/VFXData")]
public class VFXData : PooledObjectData
{
    [Header("VFXData")]
    public Sprite sprite;

    public RuntimeAnimatorController anim;
}
