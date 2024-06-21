using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(GraphicRaycaster))]
public abstract class PageUI : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected bool isOpened;

    public Action<PageUI> OnOpen;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Toggle()
    {
        if (isOpened)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public virtual void Show()
    {
        if (isOpened) return;
        StopAllCoroutines();
        
        OnOpen?.Invoke(this);
        isOpened = true;
        StartCoroutine(canvasGroup.Fade(1, 0.1f));
    }

    public virtual void Hide()
    {
        StopAllCoroutines();
        
        isOpened = false;
        StartCoroutine(canvasGroup.Fade(0f, 0.1f));
    }

    public bool IsOpen()
    {
        return isOpened;
    }
}
