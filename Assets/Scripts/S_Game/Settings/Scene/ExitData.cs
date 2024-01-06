using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitData", menuName = "ScriptableObjects/Data/Scene/ExitData")]
public class ExitData : ScriptableObject
{
    public List<ExitSettings> exitSettings;
    
    [Serializable]
    public class ExitSettings
    {
        public int id;
        public Vector2 position;
                
        public SceneLoaderEnum.Scene destination;
    }
}
