using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Inventory
{
    using AYellowpaper.SerializedCollections;
    
    public class InventorySystem : SingletonObject<InventorySystem>, IDataPersistence
    {
        [SerializedDictionary("ItemData", "Information")]
        public SerializedDictionary<ItemData, int> itemDict = new();

        [SerializeField] public ItemDatabaseData ItemDatabaseData;

        public Action<ItemData, int> OnChangedItem;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            QuestManager.Instance.QuestEvent.OnQuestClaimRewards += ClaimQuestReward;
        }

        private void OnDisable()
        {
            if (QuitUtils.isQuitting) return;
            
            QuestManager.Instance.QuestEvent.OnQuestClaimRewards -= ClaimQuestReward;
        }

        private void ClaimQuestReward(QuestInfoSO.Reward[] rewards)
        {
            Debug.Log("Claim Rewards");
            foreach (QuestInfoSO.Reward reward in rewards)
            {
                AddItem(reward.itemData, reward.number);
            }
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

        public bool UseItem(ItemData itemData, int number)
        {
            if (itemDict[itemData] >= number)
            {
                itemDict[itemData] -= number;
                return true;
            }
            
            Debug.LogError($"You have {itemDict[itemData]} {itemData} not enough {number}");
            return false;
        }
        
        public bool BuyItem(ShopItemData.ShopSingleItemData singleItemData, int number)
        {
            ItemData currencyData = GameSettings.Instance.CurrencyDict[CurrencyType.Soul];
            if (UseItem(currencyData, singleItemData.Price * number))
            {
                AddItem(singleItemData.ItemData, number);
                return true;
            }

            return false;
        }

        public void LoadData(GameData gameData)
        {
            itemDict = new();
            foreach (var item in gameData.itemInventoryDict)
            {
                itemDict.Add(ItemDatabaseData.GetItem(item.Key), item.Value);
            }
        }
        public void SaveData(ref GameData data)
        {
            data.itemInventoryDict = new();
            foreach (var item in itemDict)
            {
                data.itemInventoryDict.Add(item.Key.id, item.Value);
            }
        }
    }
}
