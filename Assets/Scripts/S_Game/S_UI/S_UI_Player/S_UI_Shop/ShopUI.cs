using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Inventory;
using UnityEngine;

public class ShopUI : PageUI
{
    private ShopItemUI     shopItemUI;
    private ShopItemDescUI shopItemDescUI;
    private ShopBuyBtn     shopBuyBtn;

    private ShopItemData.ShopSingleItemData currentItemData;

    protected override void Awake()
    {
        base.Awake();
        shopItemUI     = GetComponentInChildren<ShopItemUI>();
        shopItemDescUI = GetComponentInChildren<ShopItemDescUI>();
        shopBuyBtn     = GetComponentInChildren<ShopBuyBtn>();
        
        Hide();
    }


    private void OnEnable()
    {
        shopItemUI.OnSelectItem += UpdateSelectedShopItem;
        shopBuyBtn.OnClick      += BuyItem;
    }

    private void OnDisable()
    {
        shopItemUI.OnSelectItem -= UpdateSelectedShopItem;
        shopBuyBtn.OnClick      -= BuyItem;
    }


    private void UpdateSelectedShopItem(ShopItemData.ShopSingleItemData singleItemData)
    {
        currentItemData = singleItemData;
        shopItemDescUI.UpdateUI(singleItemData.ItemData);
    }
    
    private void BuyItem()
    {
        InventorySystem.Instance.BuyItem(currentItemData, 1);
    }

    public void PopulateShopItems(ShopItemData shopItemData)
    {
        shopItemUI.PopulateList(shopItemData.ItemDatas);
    }
}
