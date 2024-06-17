using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : PageUI
{
    [SerializeField] private ShopItemUI shopItemUI;

    public void PopulateShopItems(ShopItemData shopItemData)
    {
        shopItemUI.PopulateList(shopItemData.ItemDatas);
    }
}
