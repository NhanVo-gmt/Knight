using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : PageUI
{
    [SerializeField] TextMeshProUGUI text;

    protected override void Awake()
    {
        base.Awake();
        ToggleConversation(false);
    }

    public void ToggleConversation(bool active)
    {
        if (active) canvasGroup.alpha = 1;
        else canvasGroup.alpha = 0;
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
