using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/Data/PooledObjectData/ProjectileData")]
public class ProjectileData : PooledObjectData
{
    [Header("Projectile")] public GameObject projectile;
    public int damage;
    public float velocity;

    private Sprite sprite;
    private RuntimeAnimatorController runtimeAnim;

    #region Get

    public Sprite GetSprite()
    {
        if (!sprite) sprite = projectile.GetComponent<SpriteRenderer>().sprite;
        return sprite;
    }
    
    public RuntimeAnimatorController GetRuntimeAnim()
    {
        if (!runtimeAnim) runtimeAnim = projectile.GetComponent<Animator>().runtimeAnimatorController;
        return runtimeAnim;
    }

    public virtual void GetDirection()
    {
        //todo
    }

    public virtual void GetPrefabAfterExplosion()
    {
        //todo
    }

    #endregion
}
