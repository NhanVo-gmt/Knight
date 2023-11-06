using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleData", menuName = "ScriptableObjects/Data/PooledObjectData/ParticleData")]
public class ParticleData : PooledObjectData
{
    [Header("ParticleData")] public GameObject particleSystem;
}
