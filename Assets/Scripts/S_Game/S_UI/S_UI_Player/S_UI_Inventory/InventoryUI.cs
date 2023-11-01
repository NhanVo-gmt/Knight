using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private Transform itemRowPrefab;
    [SerializeField] private ItemSlotUI itemSlotUIPrefab;
    
    [SerializeField] private List<ItemSlotUI> itemSlot; //todo remove serialize

    private readonly int numberSlotPerRow = 3;
    private readonly int numberOfRow = 3;

    private void Awake()
    {
        CreateSlot();
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
}
