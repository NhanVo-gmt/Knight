using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Inventory;
using UnityEngine;

namespace Knight.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("Item Slot")]
        [SerializeField] private Transform content;
        [SerializeField] private Transform itemRowPrefab;
        [SerializeField] private ItemSlotUI itemSlotUIPrefab;

        [Header("Item Details")] [SerializeField]
        private ItemDetailsUI itemDetailsUI;
        
        private List<ItemSlotUI> itemSlot = new List<ItemSlotUI>(); 

        private readonly int numberSlotPerRow = 4;
        private readonly int numberOfRow = 3;

        [SerializeField] private ItemSlotUI currentItemSlot;

        private void Awake()
        {
            CreateSlot();
        }

        private void Start()
        {
            InventorySystem.Instance.OnChangedItem += UpdateSlot;
        }
        
        private void CreateSlot()
        {
            for (int i = 0; i < numberOfRow; i++)
            {
                Transform itemRowTransform = Instantiate(itemRowPrefab, content);
                for (int j = 0; j < numberSlotPerRow; j++)
                {
                    ItemSlotUI itemSlotUI = Instantiate(itemSlotUIPrefab, itemRowTransform);
                    itemSlotUI.OnClick += OnClickItemSlotUI;
                    itemSlot.Add(itemSlotUI);
                }
            }
        }

        private void OnClickItemSlotUI(ItemSlotUI itemSlotUI)
        {
            currentItemSlot = itemSlotUI;
            itemDetailsUI.SetItemDetails(currentItemSlot.currentItemData);
        }

        private void UpdateSlot(ItemData itemData, int number)
        {
            ItemSlotUI itemSlotUI = FindItemSlot(itemData);
            if (itemSlotUI != null)
            {
                Debug.Log(number);
                itemSlotUI.UpdateItemSlot(itemData, number);
            }
            else SetNewSlot(itemData, number);
        }

        private void SetNewSlot(ItemData itemData, int number)
        {
            for (int i = 0; i < itemSlot.Count; i++)
            {
                if (itemSlot[i].currentItemData == null)
                {
                    itemSlot[i].UpdateItemSlot(itemData, number);
                    return;
                }
            }
        }

        private ItemSlotUI FindItemSlot(ItemData itemData)
        {
            for (int i = 0; i < itemSlot.Count; i++)
            {
                if (itemSlot[i].currentItemData == null) break;
                
                if (itemSlot[i].currentItemData.id == itemData.id)
                {
                    return itemSlot[i];
                }
            }

            return null;
        }
    }
}
