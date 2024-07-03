using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
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

    public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> original) where TValue : ICloneable<TValue>
    {
        Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
            original.Comparer);
        foreach (KeyValuePair<TKey, TValue> entry in original)
        {
            ret.Add(entry.Key, (TValue) entry.Value.Clone());
        }
        return ret;
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

    public static string GetAssetPath(Object obj)
    {
        return AssetDatabase.GetAssetPath(obj);
    }

    public static string GetFolderAssetPath(Object obj)
    {
        return Path.GetDirectoryName(AssetDatabase.GetAssetPath(obj));
    }
}
#endif


