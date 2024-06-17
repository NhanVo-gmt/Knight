using System.Collections;
using System.Collections.Generic;
using Knight.UI;
using UnityEngine;

public class ShopHolder : NPCComponents
{
    [SerializeField] private ShopItemData shopItemData;
    
    public override void Open()
    {
        //todo open shop
        // populate item data
        ShopUI shopUI = GameCanvas.GetPage<ShopUI>();
        shopUI.PopulateShopItems(shopItemData);
        
        shopUI.Toggle();
    }
    public override void Close()
    {
        
    }
}
