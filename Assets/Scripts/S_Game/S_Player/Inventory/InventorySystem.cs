using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Inventory
{
    [Serializable]
    class InventoryItem
    {
        public ItemData itemData;
        public int number;

        public InventoryItem(ItemData itemData, int number)
        {
            this.itemData = itemData;
            this.number = number;
        }
    }
    
    public class InventorySystem : SingletonObject<InventorySystem>
    {
        [SerializeField] private List<InventoryItem> itemList = new List<InventoryItem>(); // todo remove serial

        public Action<ItemData, int> OnChangedItem;

        protected override void Awake()
        {
            base.Awake();
        }

        int GetItemIndex(ItemData itemData)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].itemData.id == itemData.id)
                {
                    return i;
                }
            }

            return -1;
        }

        public void AddItem(ItemData itemData, int number)
        {
            int itemIndex = GetItemIndex(itemData);
            if (itemIndex == -1)
            {
                itemList.Add(new InventoryItem(itemData, number));
                itemIndex = itemList.Count - 1;
            }
            else
            {
                itemList[itemIndex].number += number;
            }
            
            OnChangedItem?.Invoke(itemData, itemList[itemIndex].number);
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
