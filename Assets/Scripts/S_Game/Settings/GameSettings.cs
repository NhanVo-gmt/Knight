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
    public GameObject player;
    public int maxHealth;
    public Material playerMat;

    [Header("Player Cam")]
    public Camera playerCam;

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

    public Action OnGameInitialized;

    public void StartGame()
    {
        player.SetActive(true);
        playerCam.gameObject.SetActive(true);
        OnGameInitialized?.Invoke();
    }

    
}
