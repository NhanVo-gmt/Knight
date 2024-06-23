using System;
using System.Collections;
using System.Collections.Generic;
using DS.ScriptableObjects;
using Knight.UI;
using UnityEngine;

public class InteractionController : CoreComponent
{
    [SerializeField] [ReadOnlyInspector] bool isInteracting = false;

    [SerializeField] [ReadOnlyInspector] public bool    canRest;
    [SerializeField] [ReadOnlyInspector] public bool    canTalk;
    public                                      Vector2 restPos { get; private set; }
    
    private InputManager inputManager;
    private DialogueController dialogueController;
    
    private InteractableArea interactableArea;

    public Action OnFinishInteract;

    protected override void Awake()
    {
        base.Awake();

        canRest            = false;
        canTalk            = false;
        inputManager       = GetComponentInParent<InputManager>();
        dialogueController = GetComponentInChildren<DialogueController>();
    }

    void OnEnable() 
    {
        dialogueController.OnFinishDialogue += FinishInteract;
    }

    void OnDisable() 
    {
        dialogueController.OnFinishDialogue -= FinishInteract;
    }

    void Update() 
    {
        if (inputManager.interactionInput)
        {
            Interact();
        }
    }

    #region Interact

    void Interact()
    {
        if (canRest) return;
        if (isInteracting) return;
        
        if (interactableArea != null)
        {
            interactableArea.Interact();
        }
    }

    public void StartConversation()
    {
        if (dialogueController.currentDialogue != null)
        {
            dialogueController.StartConversation();
            isInteracting = true;
        }
    }
    
    void FinishInteract()
    {
        isInteracting = false;
        OnFinishInteract?.Invoke();
    }
    

    #region Dialogue

    public void SetDialogue(DialogueHolder holder)
    {
        canTalk                            = true;
        dialogueController.dialogueHolder  = holder;
        dialogueController.currentDialogue = holder.GetDialogue();
    }

    public void UnsetDialogue(DialogueHolder holder)
    {
        if (dialogueController.dialogueHolder == holder)
        {
            canTalk                            = false;
            dialogueController.dialogueHolder  = null;
            dialogueController.currentDialogue = null;
        }
    }
    
    #endregion

    #region Interactable Area

    public void SetInteractableArea(InteractableArea interactableArea)
    {

        this.interactableArea = interactableArea;
    }

    public void UnSetInteractableArea()
    {
        interactableArea = null;
    }

    #endregion

    #region Rest

    public void EnableResting(Vector2 restPos)
    {
        canRest = true;
        this.restPos = restPos;
    }

    public void DisableResting()
    {
        canRest = false;
    }

    #endregion
    
    #endregion


    
    
}
