using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneDesignMethods
{
    private static readonly string SceneDataPath = "Assets/ScriptableObjects/Data/SO_Scene/SceneData.asset";
    
    [MenuItem("Knight/Scene/Exit Assigning")]
    static void AssigningExitInScene()
    {
        MapData mapData = LoadSceneData();
        if (mapData == null) return;
        
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            if (gameObject.TryGetComponent<Exit>(out Exit exit))
            {
                ExitData.ExitSettings settings = mapData.GetExit(SceneManager.GetActiveScene().name, exit.id);
                exit.scene = settings.destination;
                exit.spawnPos = settings.spawnPos;
                
                Debug.Log($"Assign for game object: {gameObject.name}");
            }
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    
    static MapData LoadSceneData()
    {
        MapData data = (MapData)AssetDatabase.LoadAssetAtPath(SceneDataPath, typeof(MapData));
        if (data != null)
        {
            return data;
        }

        Debug.LogError($"There is no SceneData at path {SceneDataPath}. Please create one");
        return null;
    }

    [MenuItem("Knight/Scene/Parallax Assigning")]
    static void AutoAssigningParallax()
    {
        MapData mapData = LoadSceneData();
        if (mapData == null) return;
        
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            Parallax[] parallaxArr = gameObject.GetComponentsInChildren<Parallax>();
            if (parallaxArr.Length > 0)
            {
                foreach (Parallax parallax in parallaxArr)
                {
                    
                    AssigningValue(parallax);
                    AssigningStartPos(parallax, mapData);
                    
                    Debug.Log($"Assign for game object: {gameObject.name}");
                }
            }
        }
    }

    static void AssigningStartPos(Parallax parallax, MapData mapData)
    {
        parallax.startCamPos = mapData.GetImageStartPos(SceneManager.GetActiveScene().name);
    }

    private static Dictionary<string, float> parallaxNameDictionary = new Dictionary<string, float>()
    {
        {"Front", .7f},
        {"Mid", .75f},
        {"Back", .8f},
        {"Backback", .85f},
        {"Last", 0.9f},
        {"Sky", 1},
    };

    static void AssigningValue(Parallax parallax)
    {
        foreach (string key in parallaxNameDictionary.Keys)
        {
            if (parallax.gameObject.name.ToUpper().Equals(key.ToUpper()))
            {
                parallax.parralaxEffect = parallaxNameDictionary[key];
            }
        }
    }
}
