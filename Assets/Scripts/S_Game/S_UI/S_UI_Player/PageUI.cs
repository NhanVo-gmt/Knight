using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(GraphicRaycaster))]
public abstract class PageUI : MonoBehaviour
{
    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Toggle()
    {
        
    }

    public virtual void Show()
    {
        StartCoroutine(canvasGroup.Fade(1, 0.1f));
    }

    public virtual void Hide()
    {
        StartCoroutine(canvasGroup.Fade(0f, 0.1f));
    }
}
