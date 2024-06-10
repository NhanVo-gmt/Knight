using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemScroll : MonoBehaviour
{
    [SerializeField] private ShopItemButton itemButtonPrefab;
    [SerializeField]
    private ScrollRect scrollRect; //your scroll rect component
    [SerializeField]
    RectTransform _container; //content transform of the scrollrect

    private ShopItemButton[] ShopItemButtons;

    private void Awake()
    {
        ShopItemButtons = _container.GetComponentsInChildren<ShopItemButton>();
    }

    private void OnEnable()
    {
        foreach (ShopItemButton item in ShopItemButtons)
        {
            item.OnSelectItem += OnSelectItem;
        }
    }

    void OnSelectItem(ShopItemButton item)
    {
        EnsureVisibility(scrollRect, item.GetComponent<RectTransform>());
    }

    public void EnsureVisibility(ScrollRect scrollRect, RectTransform child, float padding=0)
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
