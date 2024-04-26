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

    private void Awake()
    {
        EmptyItemDetails();
    }

    public void EmptyItemDetails()
    {
        image.enabled = false;
        nameText.text = String.Empty;
        descriptionText.text = String.Empty;
    }

    public void SetItemDetails(ItemData itemData)
    {
        image.enabled = true;
        image.sprite = itemData.sprite;
        nameText.text = itemData.itemName;
        descriptionText.text = itemData.itemDescription;
    }
    
    
}
