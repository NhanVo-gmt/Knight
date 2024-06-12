using System;
using System.Collections;
using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;

public class InteractionController : CoreComponent
{
    [SerializeField] [ReadOnlyInspector] bool isInteracting = false;

    public bool    canRest { get; private set; }
    public Vector2 restPos { get; private set; }
    
    private InputManager inputManager;
    private DialogueController dialogueController;
    private InteractableArea interactableArea;

    protected override void Awake()
    {
        base.Awake();

        canRest = false;
        inputManager = GetComponentInParent<InputManager>();
        dialogueController = GetComponentInChildren<DialogueController>();
    }

    void OnEnable() 
    {
        dialogueController.onFinishDialogue += FinishInteract;
    }

    void OnDisable() 
    {
        dialogueController.onFinishDialogue -= FinishInteract;
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
        
        inputManager.UseInteractionInput();
        
        if (dialogueController.currentDialogue != null)
        {
            dialogueController.StartConversation();
            isInteracting = true;
        }
        else if (interactableArea != null)
        {
            interactableArea.Interact();
        }
    }
    
    void FinishInteract()
    {
        isInteracting = false;
    }
    

    #region Dialogue

    public void SetDialogue(DialogueHolder holder)
    {
        dialogueController.dialogueHolder  = holder;
        dialogueController.currentDialogue = holder.GetDialogue();
    }

    public void UnsetDialogue(DialogueHolder holder)
    {
        if (dialogueController.dialogueHolder == holder)
        {
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
