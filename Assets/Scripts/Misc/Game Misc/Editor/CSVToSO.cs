using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class CSVToSO
{
    private static readonly string CSVPath = "/Assets/Doc/ItemDatabase.csv";
    private static readonly string ItemPath = "/ScriptableObjects/Data/SO_Item/";
    private static readonly string ItemDatabasePath = "Assets/ScriptableObjects/Data/SO_Item/ItemDatabase.asset";

    [MenuItem("Knight/Generate Item Database")]
    public static void GenerateItemDatabase()
    {
        ItemDatabaseData itemDatabaseData = GetItemDatabase();
        
        Debug.Log($"Reading file from {Application.dataPath + CSVPath}");
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVPath);
        foreach (string line in allLines)
        {
            string[] splitData = line.Split(",");
            if (splitData[0] == String.Empty) continue;

            string savePath = Application.dataPath + ItemPath + splitData[1] + ".asset";
            string relativeSavePath = "Assets/" + ItemPath + splitData[1] + ".asset";
            if (!File.Exists(savePath))
            {
                ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
                itemData.id              = splitData[0];
                itemData.itemName        = splitData[1];
                itemData.itemDescription = splitData[2].Replace("\"", "");
                
                savePath = AssetDatabase.GenerateUniqueAssetPath(relativeSavePath);
                Debug.Log($"Creating item for {savePath}");
                AssetDatabase.CreateAsset(itemData, savePath);
                AssetDatabase.SaveAssets();
                
                itemDatabaseData.AddItem(itemData);
            }
            else
            {
                ItemData itemData = (ItemData)AssetDatabase.LoadAssetAtPath(relativeSavePath, typeof(ItemData));
                if (itemData)
                {
                    itemData.id              = splitData[0];
                    itemData.itemName        = splitData[1];
                    itemData.itemDescription = splitData[2].Replace("\"", "");
                    
                    Debug.Log($"Override item for {splitData[1]}");
                }
            }
        }
        
        // itemDatabaseData.SortItem();
    }

    private static ItemDatabaseData GetItemDatabase()
    {
        ItemDatabaseData itemDatabase = (ItemDatabaseData)AssetDatabase.LoadAssetAtPath(ItemDatabasePath, typeof(ItemDatabaseData));
        if (!itemDatabase)
        {
            Debug.Log("Creating new item database");
            itemDatabase = ScriptableObject.CreateInstance<ItemDatabaseData>();
            
            Debug.Log($"Creating item database for {ItemDatabasePath}");
            AssetDatabase.CreateAsset(itemDatabase, ItemDatabasePath);
            AssetDatabase.SaveAssets();
        }
        else Debug.Log($"Reading item database from {ItemDatabasePath}");
        return itemDatabase;
    }
}

#endif