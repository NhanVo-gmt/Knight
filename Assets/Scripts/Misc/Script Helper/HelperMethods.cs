using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMethods
{
    public static List<T> Reverse<T>(this List<T> list)
    {
        List<T> res = new List<T>();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            res.Add(list[i]);
        }
        return res;
    }
    
    
}
#if UNITY_EDITOR
public static class EditorHelperMethods
{
    public static List<T> FindAssets<T>(params string[] paths) where T : Object
    {
        string[] assetGUIDs = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T), paths);
        List<T>  assets     = new List<T>();
        foreach (string guid in assetGUIDs)
        {
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            T      asset     = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
            assets.Add(asset);
        }
        return assets;
    }
}
#endif


