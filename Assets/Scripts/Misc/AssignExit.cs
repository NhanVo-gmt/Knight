using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AssignExit
{
    private static readonly string SceneDataPath = "Assets/ScriptableObjects/Data/SO_Scene/SceneData.asset";
    
    [MenuItem("Knight/Assigning Exit in scene")]
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
}
