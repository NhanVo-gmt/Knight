using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public SceneLoader.Scene scene;

    public Vector2 playerPos;
    public string playerState;
    
    
    public GameData()
    {
        scene = SceneLoader.Scene.FarmScene;
        playerPos = Vector2.zero;
        playerState = "IdleState";
    }
}
