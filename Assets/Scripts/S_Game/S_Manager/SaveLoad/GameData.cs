using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string sceneName;

    public Vector2 playerPos;
    public string playerState;
    public int playerMoney;

    public List<string> bossDefeated;
    
    public GameData()
    {
        sceneName = SceneLoaderEnum.Scene.FarmScene.ToString(); 
        playerPos = Vector2.zero;
        playerState = "IdleState";
        bossDefeated = new List<string>();
    }
}
