using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Knight.Inventory;
using UnityEngine;

public class ShopUI : PageUI
{
    private ShopItemUI     shopItemUI;
    private ShopItemDescUI shopItemDescUI;
    private ShopBuyBtn     shopBuyBtn;

    private ShopItemData                    shopItemData;
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
        int selectedNumber = 1;
        if (InventorySystem.Instance.BuyItem(currentItemData, selectedNumber))
        {
            shopItemData.RemoveItem(currentItemData.ItemData, selectedNumber);
        }
        shopItemDescUI.HideUI();
        
        PopulateShopItems(shopItemData);
    }
    
    public void PopulateShopItems(ShopItemData shopItemData)
    {
        this.shopItemData = shopItemData;
        shopItemUI.PopulateList(shopItemData.GetListShopItems());
    }
}
