using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ExitData", menuName = "ScriptableObjects/Data/Scene/ExitData")]
public class ExitData : ScriptableObject
{
    public List<ExitSettings> exitSettings;
    
    [Serializable]
    public class ExitSettings
    {
        public int id;
        [FormerlySerializedAs("position")] public Vector2 spawnPos;
                
        public SceneLoaderEnum.Scene destination;
    }
    
    public ExitData.ExitSettings GetExit(int id)
    {
        foreach (ExitSettings setting in exitSettings)
        {
            if (setting.id == id)
            {
                return setting;
            }
        }

        Debug.LogError($"Can't find id in exit settings in scene {name} ");
        return null;
    }
}
