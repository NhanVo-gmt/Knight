using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DS.Enumerations;
using DS.ScriptableObjects;
using Knight.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.05f;
    [SerializeField] float typingFastSpeed = 0.01f;
    
    [SerializeField] [ReadOnlyInspector] private InGameUI   inGameUI; 
    [SerializeField] [ReadOnlyInspector] private DialogueUI dialogueUI;

    [ReadOnlyInspector] public DialogueHolder        dialogueHolder;
    [ReadOnlyInspector] public DSDialogueContainerSO currentDialogue; 
    DSDialogueSO                                     currentNode;
    
    private InputManager inputManager;
    private bool         isSpeedup = false;

    private void Awake()
    {
        inputManager = GetComponentInParent<InputManager>();
        
    }

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
        isSpeedup = false;
        string textType = "";
        foreach(char character in text)
        {
            if (inputManager.interactionInput)
            {
                inputManager.UseInteractionInput();
                isSpeedup = true;
            }
            
            textType += character;
            dialogueUI.DisplayText(textType);

            if (isSpeedup)
            {
                yield return new WaitForSeconds(typingFastSpeed);
            }
            else yield return new WaitForSeconds(typingSpeed);
        }

        dialogueUI.DisplayText(text);
        
        yield return new WaitUntil(() => IsMoveToNextDialogue());

        if (HaveNextNode())
        {
            if (IsDialogueNode())
            {
                StartCoroutine(TextTypingCoroutine(currentNode.Text));
            }
            else
            {
                switch (currentNode.DialogueType)
                {
                    case DSDialogueType.Shop:
                        OpenShop();
                        break;
                    case DSDialogueType.Quest:
                        break;
                }
            }
        }
        else
        {
            ToggleDialogueUI(false);
        }
    }

    bool IsMoveToNextDialogue()
    {
        if (inputManager.interactionInput)
        {
            inputManager.UseInteractionInput();
            return true;
        }

        return false;
    }
    
    public void ToggleDialogueUI(bool isActive)
    {
        dialogueUI.Toggle(isActive);
        inGameUI.Toggle(!isActive);
        
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
        // todo add multiple node
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

    bool IsDialogueNode()
    {
        return currentNode.DialogueType == DSDialogueType.SingleChoice ||
               currentNode.DialogueType == DSDialogueType.MultipleChoice;
    }
    
    public void OpenShop()
    {
        dialogueUI.Toggle(false);
        inGameUI.Toggle(false);
        
        ShopUI shopUI = GameCanvas.GetPage<ShopUI>();
        shopUI.PopulateShopItems(currentNode.ShopItem);
        
        // todo toggle shop tat het o trong game ui
        shopUI.Toggle();
    }
}
