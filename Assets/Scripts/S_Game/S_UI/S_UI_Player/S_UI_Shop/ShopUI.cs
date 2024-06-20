using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : PageUI
{
    private ShopItemUI shopItemUI;

    protected override void Awake()
    {
        base.Awake();
        shopItemUI = GetComponentInChildren<ShopItemUI>();
    }

    public void PopulateShopItems(ShopItemData shopItemData)
    {
        shopItemUI.PopulateList(shopItemData.ItemDatas);
    }
}
