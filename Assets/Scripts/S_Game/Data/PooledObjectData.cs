using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectData : ScriptableObject
{
    [Header("Spawn Property")]
    public Vector2 spawnPos;
    public Vector3 spawnRot;
    public bool needPlayerDirection;
    public float lifeTime;

    public T Clone<T>() where T : PooledObjectData
    {
        return (T)Instantiate(this);
    }
}
