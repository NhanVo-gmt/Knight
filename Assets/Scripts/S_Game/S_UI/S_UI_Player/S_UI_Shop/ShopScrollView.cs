using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private ShopItemButton[] children;

    private void Awake()
    {
        if (listCount > 0)
        {
            CreateItems(listCount);
            UpdateAllButtonNavigationalReferences();
        }
    }

    private void Start()
    {
        StartCoroutine(DelaySelectChild(defaultSelectIndex));
    }

    IEnumerator DelaySelectChild(int index)
    {
        yield return new WaitForSeconds(1f);

        SelectChild(index);
    }

    public void SelectChild (int index)
    {
        if (index >= children.Length) return;
        
        children[index].ObtainSelectionFocus();
    }
        

    private void UpdateAllButtonNavigationalReferences()
    {
        children = content.transform.GetComponentsInChildren<ShopItemButton>();
        if (children.Length < 2)
        {
            return;
        }

        for (int i = 0; i < children.Length; i++)
        {
            ShopItemButton item = children[i];
            Navigation navigation = item.gameObject.GetComponent<Navigation>();

            navigation.selectOnUp = GetNavigationUp(i, children);
            navigation.selectOnDown = GetNavigationDown(i, children);
        }
        
    }

    private Selectable GetNavigationUp(int index, ShopItemButton[] children)
    {
        if (index == 0)
        {
            return children[children.Length - 1].GetComponent<Selectable>();
        }

        return children[index - 1].GetComponent<Selectable>();
    }

    private Selectable GetNavigationDown(int index, ShopItemButton[] children)
    {
        if (index == children.Length - 1)
        {
            return children[0].GetComponent<Selectable>();
        }

        return children[index + 1].GetComponent<Selectable>();
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
