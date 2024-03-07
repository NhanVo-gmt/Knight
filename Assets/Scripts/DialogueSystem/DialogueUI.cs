using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup imageCanvasGroup;
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        ToggleConversation(false);
    }

    public void ToggleConversation(bool active)
    {
        if (active) imageCanvasGroup.alpha = 1;
        else imageCanvasGroup.alpha = 0;
    }

    public void StartConversation()
    {
        text.enabled = true;
        ToggleConversation(true);
    }

    public void EndConversation()
    {
        text.enabled = false;
        ToggleConversation(false);
    }

    public void DisplayText(string newText)
    {
        text.SetText(newText);
    }
}
