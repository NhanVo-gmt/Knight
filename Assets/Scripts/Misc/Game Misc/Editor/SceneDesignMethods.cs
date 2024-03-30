using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneDesignMethods
{
    private static readonly string SceneDataPath = "Assets/ScriptableObjects/Data/SO_Scene/SceneData.asset";
    private static readonly string BackgroundEffectDataPath = "Assets/ScriptableObjects/Data/SO_Scene/Background/BackgroundEffectData.asset";
    
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
                    AssigningParallaxValue(parallax);
                    AssigningStartPos(parallax, mapData);
                    
                    Debug.Log($"Assign for game object: {gameObject.name}");
                }
            }
        }
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    static void AssigningStartPos(Parallax parallax, MapData mapData)
    {
        parallax.startCamPos = mapData.GetImageStartPos(SceneManager.GetActiveScene().name);
    }

    static void AssigningParallaxValue(Parallax parallax)
    {
        BackgroundEffectData data = LoadBackgroundEffectData();
        if (data == null)
        {
            return;
        }

        SingleBackgroundEffect effect = data.GetEffect(parallax.gameObject.name);
        if (effect != null)
        {
            parallax.parralaxEffect = effect.parallax;
            return;
        }
    }
    
    static BackgroundEffectData LoadBackgroundEffectData()
    {
        BackgroundEffectData data = (BackgroundEffectData)AssetDatabase.LoadAssetAtPath(BackgroundEffectDataPath, typeof(BackgroundEffectData));
        if (data != null)
        {
            return data;
        }

        Debug.LogError($"There is no Background Effect Data at path {BackgroundEffectDataPath}");
        return null;
    }
    
    [MenuItem("Knight/Scene/Blur Assigning")]
    static void AutoAssigningBlur()
    {
        BackgroundEffectData data = LoadBackgroundEffectData();
        if (data == null) return;
        
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            if (!gameObject.GetComponent<Parallax>())
            {
                continue;
            }
            
            SingleBackgroundEffect effect = data.GetEffect(gameObject.name);
            if (effect != null)
            {
                foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
                {
                    sprite.material = effect.blurMat;
                    
                    Debug.Log($"Assign for game object: {sprite. gameObject.name}");
                }
            }
        }
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

}
