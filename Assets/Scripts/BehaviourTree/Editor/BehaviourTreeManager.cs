using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BehaviourTreeManager
{
    static string settingsPath = "Assets/Plugin/BehaviourTree/BehaviourTreeSettings.asset";
    
    public static BehaviourTreeSettings GetOrCreateSettings()
    {
        string[] guids = AssetDatabase.FindAssets("t:BehaviourTreeSettings");
        if (guids.Length > 1)
        {
            Debug.LogWarning("Already have 1 setting");
            Debug.Log(AssetDatabase.GUIDToAssetPath(guids[0]));
        }

        switch (guids.Length)
        {
            case 1:
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<BehaviourTreeSettings>(path);
            }
            case 0:
            {
                return CreateSettings();
            }
        }

        return null;
    }   

    static BehaviourTreeSettings CreateSettings()
    {
        BehaviourTreeSettings settings = ScriptableObject.CreateInstance<BehaviourTreeSettings>();
        AssetDatabase.CreateAsset(settings, settingsPath);
        AssetDatabase.SaveAssets();
        
        return settings;
    }
}
