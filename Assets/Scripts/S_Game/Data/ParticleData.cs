using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleData", menuName = "ScriptableObjects/Data/PooledObjectData/ParticleData")]
public class ParticleData : PooledObjectData
{
    [Header("Particle Data")] public GameObject particleSystem;
}
