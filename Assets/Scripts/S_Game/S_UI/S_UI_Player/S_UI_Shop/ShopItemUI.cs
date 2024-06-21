using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private Button                                buyBtn;
    [SerializeField] private List<ShopItemData.ShopSingleItemData> shopItems = new();
    private                  ShopItemButton[]                      shopItemButtons;
    
    public ShopItemButton lastSelectBtn { get; private set; }

    public Action<ShopItemData.ShopSingleItemData> OnSelectItem;

    private void Awake()
    {
        shopItemButtons = GetComponentsInChildren<ShopItemButton>();
        UpdateNavigationButtons();
    }

    private void UpdateNavigationButtons()
    {
        Button[] buttons = shopItemButtons.Select(x => x.GetComponent<Button>()).ToArray();
        
        for (int i = 0; i < buttons.Length; i++)
        {
            Navigation navigation = buttons[i].navigation;
            
            navigation.selectOnUp    = buttons[i - 1 >= 0 ? i - 1 : buttons.Length - 1];
            navigation.selectOnDown  = buttons[i + 1 >= buttons.Length ? 0 : i + 1];
            navigation.selectOnRight = buyBtn;

            buttons[i].navigation = navigation;
        }
    }


    private void OnEnable()
    {
        foreach (ShopItemButton item in shopItemButtons)
        {
            item.OnSelectItem += SelectNewItemButton;
        }
    }

    private void OnDisable()
    {
        foreach (ShopItemButton item in shopItemButtons)
        {
            item.OnSelectItem -= SelectNewItemButton;
        }
    }
    private void SelectNewItemButton(ShopItemButton itemButton)
    {
        lastSelectBtn = itemButton;
        OnSelectItem?.Invoke(itemButton.GetData());
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
            
            if (i == 0) button.ObtainSelectionFocus();
        }

        for (int i = shopItems.Count; i < shopItemButtons.Length; i++)
        {
            shopItemButtons[i].gameObject.SetActive(false);
        }
    }
}
