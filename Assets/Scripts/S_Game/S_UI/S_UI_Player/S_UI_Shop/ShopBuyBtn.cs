using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyBtn : MonoBehaviour
{
    private Button button;

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
}
