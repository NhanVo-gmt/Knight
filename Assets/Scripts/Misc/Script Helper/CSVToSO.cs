using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CSVToSO
{
    private static string CSVPath = "/Assets/Doc/ItemDatabase.csv";
    private static string SOPath = "/ScriptableObjects/Data/SO_Item/";

    [MenuItem("Knight/Generate Item Database")]
    public static void GenerateItemDatabase()
    {
        Debug.Log($"Reading file from {CSVPath}");
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVPath);
        foreach (string line in allLines)
        {
            string[] splitData = line.Split(",");
            if (splitData[0] == String.Empty) continue;

            string savePath = Application.dataPath + SOPath + splitData[1] + ".asset";
            string relativeSavePath = "Assets/" + SOPath + splitData[1] + ".asset";
            if (!File.Exists(savePath))
            {
                ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
                itemData.id = splitData[0];
                itemData.itemName = splitData[1];
                itemData.itemDescription = splitData[2].Replace("\"", "");
                
                savePath = AssetDatabase.GenerateUniqueAssetPath(savePath);
                Debug.Log($"Creating file for {savePath}");
                AssetDatabase.CreateAsset(itemData, savePath);
                AssetDatabase.SaveAssets();
            }
            else
            {
                ItemData itemData = (ItemData)AssetDatabase.LoadAssetAtPath(relativeSavePath, typeof(ItemData));
                if (itemData)
                {
                    itemData.id = splitData[0];
                    itemData.itemName = splitData[1];
                    itemData.itemDescription = splitData[2].Replace("\"", "");
                    
                    Debug.Log($"Override file for {splitData[1]}");
                }
            }
        }
    }
}
