using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : CoreComponent
{
    InputManager inputManager;

    DialogueController dialogueController;
    public bool canRest { get; private set; }

    [SerializeField] bool isInteracting = false;

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

    void Interact()
    {
        if (canRest) return;
        
        inputManager.UseInteractionInput();
        
        if (isInteracting) return;

        if (dialogueController.currentDialogue != null)
        {
            dialogueController.StartConversation();
            isInteracting = true;
        }
    }

    #region Dialogue

    public void SetDialogue(Dialogue dialogue)
    {
        dialogueController.currentDialogue = dialogue;
    }

    public void UnsetDialogue(Dialogue dialogue)
    {
        if (dialogueController.currentDialogue == dialogue)
        {
            dialogueController.currentDialogue = null;
        }
    }
    
    #endregion

    #region Rest

    public void EnableResting()
    {
        canRest = true;
    }

    public void DisableResting()
    {
        canRest = false;
    }

    #endregion
    
    void FinishInteract()
    {
        isInteracting = false;
    }
}
