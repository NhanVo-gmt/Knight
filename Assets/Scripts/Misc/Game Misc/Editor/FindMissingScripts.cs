using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class FindMissingScripts
{
    [MenuItem("Knight/Missing Scripts/Find missing script in project", false, 11)]
    static void FindingMissingScriptsInProjectMenuItem()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
            .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();
        foreach (string path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            foreach (Component component in prefab.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.LogError("Prefab found with missing scripts: " + path, prefab);
                    break;
                }
            }
        }
    }

    [MenuItem("Knight/Missing Scripts/Find missing scripts in scene", false, 10)]
    static void FindingMissingScriptsInScene()
    {
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            foreach (Component component in gameObject.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.LogError("Game Object found with missing scripts: " + gameObject.name, gameObject);
                    break;
                }
            }
        }
    }
}
