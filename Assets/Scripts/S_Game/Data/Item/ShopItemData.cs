using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

public enum CurrencyType
{
    Soul,
    Artifact
}

[CreateAssetMenu(fileName = "ShopItems", menuName = "ScriptableObjects/Data/Shop")]
public class ShopItemData : ScriptableObject
{
    [Serializable]
    public class ShopSingleItemData : ICloneable<ShopSingleItemData>
    {
        public ItemData           ItemData;
        public int                Number;
        public CurrencyType       CurrencyType = CurrencyType.Soul;
        public int                Price;
        
        public ShopSingleItemData Clone()
        {
            return new ShopSingleItemData()
            {
                ItemData     = this.ItemData,
                Number       = this.Number,
                CurrencyType = this.CurrencyType,
                Price        = this.Price,
            };
        }
    }

    [SerializedDictionary("Item Data", "Desc")]
    [SerializeField] private SerializedDictionary<ItemData, ShopSingleItemData> itemDictionary = new();
    
    public Dictionary<ItemData, ShopSingleItemData> ItemDictionary;

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var item in itemDictionary)
        {
            if (item.Key != null)
            {
                item.Value.ItemData = item.Key;
            }
        }

        ItemDictionary = itemDictionary.Clone();
    }
#endif

    public void RemoveItem(ItemData itemData, int number)
    {
        if (ItemDictionary.TryGetValue(itemData, out ShopSingleItemData item))
        {
            item.Number -= number;
            if (item.Number <= 0)
            {
                ItemDictionary.Remove(itemData);
            }
        }
    }

    public List<ShopSingleItemData> GetListShopItems()
    {
        return ItemDictionary.Values.ToList();
    }

    public void Save()
    {
        //todo
    }

    public void Load()
    {
        //todo
    }
}
