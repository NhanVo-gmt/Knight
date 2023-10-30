using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Database
{
    public class ItemDatabase : SingletonObject<ItemDatabase>
    {
        [Serializable]
        class Item
        {
            public string Name;
            public string Id;
            [TextArea(5, 10)] public string Description;
            public ItemData itemData;

            public void AssignId()
            {
                this.Id = Guid.NewGuid().ToString();
            }
        }

        [SerializeField] private List<Item> itemDatabaseList = new List<Item>();

        protected override void Awake()
        {
            base.Awake();
            AssignIdForItem();
            
            foreach (Item item in itemDatabaseList)
            {
                item.itemData.AssignId(item.Id);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Assign Id For Item")]
        void AssignIdForItem()
        {
            foreach (Item item in itemDatabaseList)
            {
                if (string.IsNullOrWhiteSpace(item.Id))
                {
                    item.AssignId();
                }
            }
        }
#endif  

        public ItemData GetItemFromId(string id)
        {
            foreach (Item item in itemDatabaseList)
            {
                if (item.Id == id)
                {
                    return item.itemData;
                }
            }
            
            Debug.LogError("There is no item with this id");
            return null;
        }
    }
}
