using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Data/Scene/MapData")]
public class MapData : ScriptableObject
{
    public List<SceneSetting> sceneSettings;
    
    [Serializable]
    public class SceneSetting
    {
        public SceneLoaderEnum.Scene scene;
        public Vector2 imageStartPos;
        public ExitData exit;
    }

    public Vector2 GetImageStartPos(string scene)
    {
        foreach (MapData.SceneSetting setting in sceneSettings)
        {
            if (scene.Equals(setting.scene.ToString()))
            {
                return setting.imageStartPos;
            }
        }

        Debug.LogError($"Can't get image start pos: {scene}");
        return Vector2.zero;
    }

    public ExitData.ExitSettings GetExit(string scene, int id)
    {
        foreach (MapData.SceneSetting setting in sceneSettings)
        {
            if (scene.Equals(setting.scene.ToString()))
            {
                return setting.exit.GetExit(id);
            }
        }

        Debug.LogError($"Can't find the following scene: {scene}");
        return null;
    }
}
