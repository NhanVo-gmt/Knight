using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    public GameObject playerUI;

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
        Instantiate(player, Vector2.zero, Quaternion.identity);
        Instantiate(playerCam, Vector2.zero, Quaternion.identity);
        Instantiate(playerUI, Vector2.zero, Quaternion.identity);

        OnGameInitialized?.Invoke();
    }

    [FormerlySerializedAs("sceneData")] [Header("Scene")] 
    public MapData mapData;

    public ExitData.ExitSettings GetExit(string scene, int id)
    {
        return mapData.GetExit(scene, id);
    }

}
