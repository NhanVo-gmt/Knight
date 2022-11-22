using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectData : ScriptableObject
{
    public GameObject pooledObject;
    public bool needPlayerDirection;
    public Vector2 pooledObjectSpawnPos;
}
