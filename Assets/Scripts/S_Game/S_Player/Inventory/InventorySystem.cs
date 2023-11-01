using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Inventory
{
    class InventoryItem
    {
        public ItemData itemData;
        public int number;
    }
    
    public class InventorySystem : SingletonObject<InventorySystem>
    {
        private List<InventoryItem> itemList = new List<InventoryItem>();

        public Action<ItemData, int> OnChangedItem;

        protected override void Awake()
        {
            base.Awake();
        }

        public void AddItem(ItemData itemData, int number)
        {
            foreach (InventoryItem item in itemList)
            {
                if (item.itemData.id == itemData.id)
                {
                    item.number += number;
                    break;
                }
            }
            
            OnChangedItem.Invoke(itemData, number);
        }

        public void UseItem(ItemData itemData, int number)
        {
            foreach (InventoryItem item in itemList)
            {
                if (item.itemData.id == itemData.id)
                {
                    item.number -= number;
                    break;
                }
            }
            OnChangedItem.Invoke(itemData, number);
        }
    }
}
