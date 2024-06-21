using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] [ReadOnlyInspector] private ShopItemData.ShopSingleItemData data;

    [SerializeField] private Image image;
    [SerializeField] private Image priceImage;
    [SerializeField] private TextMeshProUGUI priceText;
    
    public Action<ShopItemButton> OnSelectItem;

    public void Populate(ShopItemData.ShopSingleItemData data)
    {
        this.data = data;
        UpdateUI();
    }

    public ShopItemData.ShopSingleItemData GetData()
    {
        return data;
    }

    void UpdateUI()
    {
        image.sprite = data.ItemData.sprite;
        priceText.SetText(data.Price.ToString());
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        OnSelectItem?.Invoke(this);
    }

    public void ObtainSelectionFocus()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        OnSelectItem?.Invoke(this);
    }
}
