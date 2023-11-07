using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetItemDetails(ItemData itemData)
    {
        image.sprite = itemData.sprite;
        nameText.text = itemData.itemName;
        descriptionText.text = itemData.itemDescription;
    }
    
    
}
