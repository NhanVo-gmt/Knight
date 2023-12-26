using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/Data/Item/Itemdatabase")]
public class ItemDatabaseData : ScriptableObject
{
    [SerializeField] private List<ItemData> itemDataList = new List<ItemData>(); //todo for visibility
    private Dictionary<int, ItemData> itemDataDictionary = new Dictionary<int, ItemData>(); // used to query
#if  UNITY_EDITOR
    public void AddItem(ItemData itemData)
    {
        itemDataList.Add(itemData);
    }

    public void SortItem()
    {
        itemDataList.Sort((x,y) => x.id.CompareTo(y.id));
        UpdateDictionary();
    }

    private void OnValidate()
    {
        UpdateDictionary();
    }

    private void UpdateDictionary()
    {
        itemDataDictionary.Clear();
        
        itemDataDictionary = itemDataList.ToList().ToDictionary(x => x.id, x => x);
        Debug.Log("Updating dictionary");
    }
#endif

    public ItemData GetItem(int id)
    {
        if (itemDataDictionary.TryGetValue(id, out ItemData itemData))
        {
            return itemData;
        }

        Debug.LogError("There is no items on the dictionary");
        return null;
    }

}
