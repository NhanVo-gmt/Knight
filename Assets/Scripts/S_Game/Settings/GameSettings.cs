using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : SingletonObject<GameSettings>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [Header("Player")]
    public int maxHealth;
    public Material playerMat;

    [Header("KnockBack")]
    public float PlayerKnockbackAmount = 100;
    public float WeakKnockbackAmount = 200;
    public float StrongKnockbackAmount = 10000;

    [Header("Drop")] 
    public PooledObjectData soul;

    [Header("Flash")] 
    public Material flashMat;
    public float flashCoolDown;

    public Material flashGlowMat;
}
