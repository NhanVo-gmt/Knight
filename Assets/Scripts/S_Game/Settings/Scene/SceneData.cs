using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/Data/Scene/SceneData")]
public class SceneData : ScriptableObject
{
    public List<SceneSetting> sceneSettings;
    
    [Serializable]
    public class SceneSetting
    {
        public SceneLoaderEnum.Scene scene;
        public ExitData exit;
    }
    
    

    public ExitData.ExitSettings GetExit(string scene, int id)
    {
        foreach (SceneData.SceneSetting setting in sceneSettings)
        {
            if (scene.Equals(setting.scene.ToString()))
            {
                return setting.exit.GetExit(id);
            }
        }

        Debug.LogError($"Can't find the following scene {scene}");
        return null;
    }
}
