using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneDesignMethods
{
    private static readonly string SceneDataPath = "Assets/ScriptableObjects/Data/SO_Scene/SceneData.asset";
    
    [MenuItem("Knight/Scene/Exit Assigning ")]
    static void AssigningExitInScene()
    {
        SceneData sceneData = LoadSceneData();
        if (sceneData == null) return;
        
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            if (gameObject.TryGetComponent<Exit>(out Exit exit))
            {
                ExitData.ExitSettings settings = sceneData.GetExit(SceneManager.GetActiveScene().name, exit.id);
                exit.scene = settings.destination;
                exit.spawnPos = settings.spawnPos;
                
                Debug.Log($"Assign for game object: {gameObject.name}");
            }
        }
    }

    
    static SceneData LoadSceneData()
    {
        SceneData data = (SceneData)AssetDatabase.LoadAssetAtPath(SceneDataPath, typeof(SceneData));
        if (data != null)
        {
            return data;
        }

        Debug.LogError($"There is no SceneData at path {SceneDataPath}. Please create one");
        return null;
    }

    [MenuItem("Knight/Scene/Parallax Assigning")]
    static void AssigningStartPosForParallax()
    {
        SceneData sceneData = LoadSceneData();
        if (sceneData == null) return;
        
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            Parallax[] parallaxArr = gameObject.GetComponentsInChildren<Parallax>();
            if (parallaxArr.Length > 0)
            {
                foreach (Parallax parallax in parallaxArr)
                {
                    parallax.startCamPos = sceneData.GetImageStartPos(SceneManager.GetActiveScene().name);
                    Debug.Log($"Assign for game object: {gameObject.name}");
                }
            }
        }
    }
}
