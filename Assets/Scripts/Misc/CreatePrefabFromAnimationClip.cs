using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CreatePrefabFromAnimationClip : MonoBehaviour
{
    static string PooledObjectPath = "Assets/Game/Prefabs/PooledObject/PooledObject.prefab";
    static AnimationClip clipSelected;

    [MenuItem("Knight/Create empty prefab from animation clip")]
    static void CreatePrefab()
    {
        clipSelected = Selection.activeObject as AnimationClip;
        if (clipSelected == null) 
        {
            Debug.Log("You haven't selected an animation clip");
            return;
        }
        
        GameObject pooledObject = GetPooledObjectFromPath();
        if (pooledObject == null) return;

        CreatePrefab(pooledObject, GetPathFromSelection());
    }

    static GameObject GetPooledObjectFromPath()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath(PooledObjectPath, typeof(GameObject)) as GameObject;
        if (prefab == null)
        {
            Debug.Log("There is no prefab at " + PooledObjectPath);
        }
        else
        {
            return Instantiate(prefab);
        }
        return null;
    }

    static string GetPathFromSelection()
    {
        return Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeInstanceID));
    }

    static void CreatePrefab(GameObject pooledPrefab, string path)
    {
        string createdPath = path + "/" + clipSelected.name + ".prefab";

        createdPath = AssetDatabase.GenerateUniqueAssetPath(createdPath);

        bool prefabSuccess;
        //GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(pooledPrefab, createdPath, InteractionMode.UserAction, out prefabSuccess);
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(pooledPrefab, createdPath, out prefabSuccess);
        if (prefabSuccess)
        {
            Debug.Log("Successfully created a prefab");

            AddAnimatorComponentToPrefab(prefab, path);
        }
        else
        {
            Debug.Log("Failed to create a prefab");
        }
    }

    static void AddAnimatorComponentToPrefab(GameObject prefab, string path)
    {
        string createdPath = path + "/" + clipSelected.name + ".controller";
        var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(createdPath);
        controller.AddMotion(clipSelected);
        prefab.GetComponent<Animator>().runtimeAnimatorController = controller;
    }
}
