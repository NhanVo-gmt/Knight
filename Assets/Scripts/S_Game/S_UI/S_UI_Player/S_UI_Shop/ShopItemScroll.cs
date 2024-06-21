using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopItemScroll : MonoBehaviour
{
    [SerializeField] RectTransform content; 

    private ScrollRect scrollRect; 
    private ShopItemButton[] shopItemButtons;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        shopItemButtons = content.GetComponentsInChildren<ShopItemButton>();
    }

    private void OnEnable()
    {
        foreach (ShopItemButton item in shopItemButtons)
        {
            item.OnSelectItem += OnSelectItem;
        }
    }

    private void OnDisable()
    {
        foreach (ShopItemButton item in shopItemButtons)
        {
            item.OnSelectItem -= OnSelectItem;
        }
    }
    
    

    void OnSelectItem(ShopItemButton item)
    {
        EnsureVisibility(item.GetComponent<RectTransform>());
    }

    public void EnsureVisibility(RectTransform child, float padding=0)
    {
        Debug.Assert(child.parent == scrollRect.content,
            "EnsureVisibility assumes that 'child' is directly nested in the content of 'scrollRect'");

        float viewportHeight = scrollRect.viewport.rect.height;
        Vector2 scrollPosition = scrollRect.content.anchoredPosition;

        float elementTop = child.anchoredPosition.y;
        float elementBottom = elementTop - child.rect.height;

        float visibleContentTop = -scrollPosition.y - padding;
        float visibleContentBottom = -scrollPosition.y - viewportHeight + padding;

        float scrollDelta =
            elementTop > visibleContentTop ? visibleContentTop - elementTop :
            elementBottom < visibleContentBottom ? visibleContentBottom - elementBottom :
            0f;

        scrollPosition.y += scrollDelta;
        scrollRect.content.anchoredPosition = scrollPosition;
    }
}
