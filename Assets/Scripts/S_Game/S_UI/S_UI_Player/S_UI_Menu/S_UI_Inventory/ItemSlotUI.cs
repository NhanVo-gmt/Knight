using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private TextMeshProUGUI numberText;

    public Action<ItemSlotUI> OnClick;

    public ItemData currentItemData { get; private set; }

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        numberText = GetComponentInChildren<TextMeshProUGUI>();
        image.enabled = false;
        currentItemData = null;
    }

    public void UpdateItemSlot(ItemData itemData, int number)
    {
        if (number <= 0)
        {
            RemoveItemSlot();
            return;
        }
        
        currentItemData = itemData;
        image.enabled = true;
        image.sprite = currentItemData.sprite;
        numberText.text = number.ToString();
    }

    private void RemoveItemSlot()
    {
        currentItemData = null;
        image.enabled = false;
        image.sprite = null;
        numberText.text = String.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItemData != null)
            OnClick?.Invoke(this);
    }
}
