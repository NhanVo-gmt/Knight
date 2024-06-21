using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Inventory
{
    using AYellowpaper.SerializedCollections;
    
    public class InventorySystem : SingletonObject<InventorySystem>
    {
        [SerializedDictionary("ItemData", "Information")]
        public SerializedDictionary<ItemData, int> itemDict = new();

        public Action<ItemData, int> OnChangedItem;

        protected override void Awake()
        {
            base.Awake();
        }

        public void AddItem(ItemData itemData, int number)
        {
            if (!itemDict.ContainsKey(itemData))
            {
                itemDict[itemData] = 0;
            }
            itemDict[itemData] += number;
            
            OnChangedItem?.Invoke(itemData, itemDict[itemData]);
        }
        
        public void BuyItem(ShopItemData.ShopSingleItemData singleItemData, int number)
        {
            // ItemData currencyData = GameS
        }
            
    }
}
