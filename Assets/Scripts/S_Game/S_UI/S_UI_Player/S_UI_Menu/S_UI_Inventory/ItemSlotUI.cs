using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI numberText;

    public ItemData currentItemData { get; private set; }

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        numberText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateItemSlot(ItemData itemData, int number)
    {
        if (number <= 0)
        {
            RemoveItemSlot();
            return;
        }
        
        currentItemData = itemData;
        image.sprite = currentItemData.itemSprite;
        if (number == 1)
        {
            numberText.text = String.Empty;
        }
        else
        {
            numberText.text = number.ToString();
        }
    }

    private void RemoveItemSlot()
    {
        currentItemData = null;
        image.sprite = null;
        numberText.text = String.Empty;
    }
}
