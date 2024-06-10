using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemButton : MonoBehaviour, ISelectHandler
{
    public Action<ShopItemButton> OnSelectItem;
    
    public void OnSelect(BaseEventData eventData)
    {
        OnSelectItem?.Invoke(this);
    }
}
