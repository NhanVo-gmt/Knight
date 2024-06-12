using System.Collections;
using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using System;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] DSDialogueContainerSO dialogue;

    public Action OnFinishDialogue;
    
    public DSDialogueContainerSO GetDialogue()
    {
        return dialogue;
    }

    public void EndConversation()
    {
        OnFinishDialogue?.Invoke();
    }
}
