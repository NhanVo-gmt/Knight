using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private List<ShopItemData.ShopSingleItemData> shopItems = new();
    private                  ShopItemButton[]                      shopItemButtons;

    private void Awake()
    {
        shopItemButtons = GetComponentsInChildren<ShopItemButton>();
    }

    public void PopulateList(List<ShopItemData.ShopSingleItemData> shopItems)
    {
        this.shopItems = shopItems;
        UpdateItemListUI();
    }

    void UpdateItemListUI()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            ShopItemButton button = shopItemButtons[i];
            
            button.Populate(shopItems[i]);
            
            button.gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopItemButtons.Length; i++)
        {
            shopItemButtons[i].gameObject.SetActive(false);
        }
    }
}
