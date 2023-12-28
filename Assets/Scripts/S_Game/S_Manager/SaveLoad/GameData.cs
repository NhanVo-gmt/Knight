using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public SceneLoader.Scene scene;
    
    
    public GameData()
    {
        scene = SceneLoader.Scene.FarmScene;
    }
}
