using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseData : ScriptableObject
{
    [SerializeField] private List<ItemData> itemDataList = new List<ItemData>();

    public ItemData GetItem(string id)
    {
        foreach (ItemData item in itemDataList)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        Debug.LogError("There is no items on the list");
        return null;
    }

    public string GetId(ItemData itemData)
    {
        foreach (ItemData item in itemDataList)
        {
            if (item == itemData)
            {
                return item.id;
            }
        }

        Debug.LogError("There is no items on the list");
        return null;
    }
}
