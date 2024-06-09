using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShopItemButton : MonoBehaviour, ISelectHandler, IPointerClickHandler, ISubmitHandler
{
    [SerializeField] private TextMeshProUGUI itemName;
    
    [SerializeField] private ShopItemButtonEvent _onSelectEvent;
    [SerializeField] private ShopItemButtonEvent _onSubmitEvent;
    [SerializeField] private ShopItemButtonEvent _onClickEvent;

    public ShopItemButtonEvent OnSelectEvent
    {
        get => _onSelectEvent;
        set => _onSelectEvent = value;
    }
    
    public ShopItemButtonEvent OnSubmitEvent
    {
        get => _onSubmitEvent;
        set => _onSubmitEvent = value;
    }
    
    public ShopItemButtonEvent OnClickEvent
    {
        get => _onClickEvent;
        set => _onClickEvent = value;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelectEvent?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClickEvent?.Invoke(this);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _onSubmitEvent?.Invoke(this);
    }

    public void ObtainSelectionFocus()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        _onSelectEvent?.Invoke(this);
    }
}

[System.Serializable]
public class ShopItemButtonEvent : UnityEvent<ShopItemButton>
{
}
