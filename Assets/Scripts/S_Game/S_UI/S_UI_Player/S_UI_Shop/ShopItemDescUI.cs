using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDescUI : MonoBehaviour
{
    [SerializeField] private Image           itemImg;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void HideUI()
    {
        StartCoroutine(canvasGroup.FadeOut());
    }

    public void UpdateUI(ItemData data)
    {
        StartCoroutine(canvasGroup.FadeIn());
        
        itemImg.sprite = data.sprite;
        title.SetText(data.itemName);
        description.SetText(data.itemDescription);
    }
}
