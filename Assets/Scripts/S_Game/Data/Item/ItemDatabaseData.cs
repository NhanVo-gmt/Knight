using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/Data/Item/Itemdatabase")]
public class ItemDatabaseData : ScriptableObject
{
    [SerializeField] private List<ItemData> itemDataList = new List<ItemData>();

    public void AddItem(ItemData itemData)
    {
        itemDataList.Add(itemData);
        
    }

    public void SortItem()
    {
        itemDataList.Sort((x,y) => x.id.CompareTo(y.id));
    }

    public ItemData GetItem(int id)
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

    public int GetId(ItemData itemData)
    {
        foreach (ItemData item in itemDataList)
        {
            if (item == itemData)
            {
                return item.id;
            }
        }

        Debug.LogError("There is no items on the list");
        return -1;
    }

}
