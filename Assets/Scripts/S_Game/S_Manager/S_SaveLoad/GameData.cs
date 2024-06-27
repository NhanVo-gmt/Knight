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

    [Header("NPC")]
    public SerializedDictionary<string, NPCSaveData> npcDict = new();
    
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
    
    [Serializable]
    public class NPCSaveData
    {
        public string                                                        Id;
        public SerializedDictionary<string, ShopItemData.ShopSingleItemData> ShopSaveData = new();

        public NPCSaveData(string id)
        {
            Id           = id;
            ShopSaveData = new();
        }
    }
}
