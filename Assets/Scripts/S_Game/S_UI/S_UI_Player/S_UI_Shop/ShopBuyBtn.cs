using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopBuyBtn : MonoBehaviour, ISelectHandler
{
    [SerializeField] private ShopItemUI shopItemUI;
    private                  Button     button;

    public Action OnClick;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(BuyItem);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void BuyItem()
    {
        OnClick?.Invoke();
    }

    public void Disable()
    {
        button.interactable = false;
    }

    public void Enable()
    {
        button.interactable = true;
    }
    public void OnSelect(BaseEventData eventData)
    {
        UpdateNavigationButton();
    }
    
    private void UpdateNavigationButton()
    {
        if (shopItemUI.lastSelectBtn == null) return;
        
        Navigation navigation = button.navigation;
        navigation.selectOnLeft = shopItemUI.lastSelectBtn.GetComponent<Button>();

        button.navigation = navigation;
    }
}
