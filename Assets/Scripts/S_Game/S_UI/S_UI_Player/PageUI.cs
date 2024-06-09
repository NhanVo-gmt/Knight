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
        
        isOpened = true;
        StartCoroutine(canvasGroup.Fade(1, 0.1f));
    }

    public virtual void Hide()
    {
        if (!isOpened) return;
        StopAllCoroutines();
        
        isOpened = false;
        StartCoroutine(canvasGroup.Fade(0f, 0.1f));
    }

    public bool IsOpen()
    {
        return isOpened;
    }
}
