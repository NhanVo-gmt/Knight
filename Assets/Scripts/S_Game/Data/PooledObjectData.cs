using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectData : ScriptableObject
{
    [Header("Pooled Object")]
    public GameObject pooledObject;
    public bool needPlayerDirection;
    
    [Header("Spawn Property")]
    public Vector2 spawnPos;
    public Vector3 spawnRot;
    public float lifeTime;
}
