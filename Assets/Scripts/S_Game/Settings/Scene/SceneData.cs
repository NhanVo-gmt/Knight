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
}
