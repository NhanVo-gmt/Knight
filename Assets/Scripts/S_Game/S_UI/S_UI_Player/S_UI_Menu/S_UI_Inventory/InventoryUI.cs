using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Inventory;
using UnityEngine;

namespace Knight.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private Transform itemRowPrefab;
        [SerializeField] private ItemSlotUI itemSlotUIPrefab;
        
        [SerializeField] private List<ItemSlotUI> itemSlot; //todo remove serialize

        private readonly int numberSlotPerRow = 4;
        private readonly int numberOfRow = 3;
        

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
                    itemSlot.Add(Instantiate(itemSlotUIPrefab, itemRowTransform));
                }
            }
        }

        private void UpdateSlot(ItemData itemData, int number)
        {
            ItemSlotUI itemSlotUI = FindItemSlot(itemData);
            if (itemSlotUI != null)
            {
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
