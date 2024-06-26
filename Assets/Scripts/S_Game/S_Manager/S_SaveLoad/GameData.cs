using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public class GameData
{
    [Header("Player")]
    public string sceneName;
    public Vector2 playerPos;
    public string playerState;
    
    
    [Header("Inventory")]
    public SerializedDictionary<string, int> itemInventoryDict = new();

    public List<string> bossDefeated;
    
    public GameData()
    {
        sceneName         = "FarmScene";
        playerPos         = Vector2.zero;
        playerState       = "IdleState";
        
        itemInventoryDict = new();
        bossDefeated      = new List<string>();
    }
}
