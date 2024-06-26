using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/Data/Item/Itemdatabase")]
public class ItemDatabaseData : ScriptableObject
{
    [SerializedDictionary("Id", "Item Data")]
    [SerializeField] private SerializedDictionary<string, ItemData> itemDataDictionary = new(); // used to query
#if  UNITY_EDITOR

    public void AddItem(ItemData itemData)
    {
        itemDataDictionary.Add(itemData.id, itemData);
    }
    
    [ContextMenu("Update Dictionary")]
    private void UpdateDictionary()
    {
        itemDataDictionary.Clear();

        string         folderPath = EditorHelperMethods.GetFolderAssetPath(this);
        List<ItemData> itemDataList  = EditorHelperMethods.FindAssets<ItemData>(folderPath);
        itemDataDictionary = new(itemDataList.ToDictionary(x => x.id, y => y));
        
        Debug.Log("Updating dictionary");
    }
#endif

    public ItemData GetItem(string id)
    {
        if (itemDataDictionary.TryGetValue(id, out ItemData itemData))
        {
            return itemData;
        }

        Debug.LogError("There is no items on the dictionary");
        return null;
    }

}
