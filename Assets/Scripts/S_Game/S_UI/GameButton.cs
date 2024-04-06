using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image[] buttonImgs;

    private void Awake()
    {
        buttonImgs = GetComponentsInChildren<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToggleImages(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        ToggleImages(false);
    }

    void ToggleImages(bool active)
    {
        for (int i = 0; i < buttonImgs.Length; i++)
        {
            buttonImgs[i].enabled = active;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayButtonClip();
    }
}
