using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DS.ScriptableObjects;
using Knight.UI;
using UnityEngine.Serialization;

public class DialogueController : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.05f;
    
    [SerializeField] [ReadOnlyInspector] private InGameUI   inGameUI; 
    [SerializeField] [ReadOnlyInspector] private DialogueUI dialogueUI;

    [ReadOnlyInspector] public DialogueHolder        dialogueHolder;
    [ReadOnlyInspector] public DSDialogueContainerSO currentDialogue; 
    DSDialogueSO                                     currentNode;

    private void Start()
    {
        inGameUI   = GameCanvas.GetPage<InGameUI>();
        dialogueUI = GameCanvas.GetPage<DialogueUI>();
    }

    public Action onFinishDialogue;

    public void StartConversation()
    {
        ToggleDialogueUI(true);
        currentNode = currentDialogue.GetStartingDialogue();

        StartCoroutine(TextTypingCoroutine(currentNode.Text));
    }

    IEnumerator TextTypingCoroutine(string text)
    {
        string textType = "";
        foreach(char character in text)
        {
            textType += character;
            dialogueUI.DisplayText(textType);
            yield return new WaitForSeconds(typingSpeed);
        }

        dialogueUI.DisplayText(text);
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        if (HaveNextNode())
        {
            StartCoroutine(TextTypingCoroutine(currentNode.Text));
        }
        else
        {
            ToggleDialogueUI(false);
        }
    }
    
    public void ToggleDialogueUI(bool isActive)
    {
        dialogueUI.gameObject.SetActive(isActive);
        inGameUI.gameObject.SetActive(!isActive);
        
        if (isActive)
        {
            dialogueUI.StartConversation();
        }
        else
        {
            dialogueUI.EndConversation();
            
            onFinishDialogue?.Invoke();
            
            dialogueHolder.EndConversation();
        }
    }


    bool HaveNextNode()
    {
        currentNode = currentNode.GetNextDialogueByIndex(0);

        if (currentNode == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
