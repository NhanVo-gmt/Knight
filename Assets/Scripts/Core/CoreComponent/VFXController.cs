using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : CoreComponent
{
    ObjectPoolingManager poolingManager;
    
    protected override void Awake()
    {
        base.Awake();

        poolingManager = FindObjectOfType<ObjectPoolingManager>();
    }

    public void SpawnVFX(GameObject prefab, Vector2 position, Vector2 direction)
    {
    }
}
