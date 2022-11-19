using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : CoreComponent
{
    ObjectPooling poolingManager;
    Movement movement;
    
    protected override void Awake()
    {
        base.Awake();

        poolingManager = FindObjectOfType<ObjectPooling>();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
    }

    public GameObject SpawnVFX(VFXData data)
    {
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(data.vfx);
        SetVFXPosition(spawnedPrefab, data);
        SetVFXRotation(spawnedPrefab, data);

        return spawnedPrefab;
    }

    void SetVFXPosition(GameObject vfx, VFXData data)
    { 
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            vfx.transform.position = new Vector2(-data.vfxSpawnPos.x, data.vfxSpawnPos.y);
        }
        else
        {
            vfx.transform.position = data.vfxSpawnPos;
        }

        vfx.transform.position += transform.position;
    }

    void SetVFXRotation(GameObject vfx, VFXData data)
    {
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            vfx.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            vfx.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
