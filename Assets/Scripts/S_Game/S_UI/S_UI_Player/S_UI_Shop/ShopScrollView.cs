using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject _prefabItem;

    [Space(10)] 
    [Header("Scroll View Event")] 
    [SerializeField] private ShopItemButtonEvent _eventItemClicked;
    [SerializeField] private ShopItemButtonEvent _eventItemSelected;
    [SerializeField] private ShopItemButtonEvent _eventItemSubmitted;

    [Space(10)] 
    [Header("Default Selected Index")] 
    [SerializeField] private int defaultSelectIndex = 0;

    [Space(10)]
    [Header("For Testing only")]
    [SerializeField] private int listCount = 1;

    private void Awake()
    {
        if (listCount > 0)
        {
            CreateItems(listCount);
        }
    }

    private void CreateItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateItem();
        }
    }

    private void CreateItem()
    {
        GameObject item = Instantiate(_prefabItem, content.transform);

        ShopItemButton itemButton = item.GetComponent<ShopItemButton>();
        itemButton.OnClickEvent.AddListener((ShopItemButton) => HandleEventClickItem(itemButton));
        itemButton.OnSelectEvent.AddListener((ShopItemButton) => HandleEventSelectItem(itemButton));
        itemButton.OnSubmitEvent.AddListener((ShopItemButton) => HandleEventSubmitItem(itemButton));
    }

    private void HandleEventSubmitItem(ShopItemButton itemButton)
    {
        _eventItemSubmitted?.Invoke(itemButton);
    }

    private void HandleEventSelectItem(ShopItemButton itemButton)
    {
        _eventItemSelected?.Invoke(itemButton);
    }

    private void HandleEventClickItem(ShopItemButton itemButton)
    {
        _eventItemClicked?.Invoke(itemButton);
    }
}
