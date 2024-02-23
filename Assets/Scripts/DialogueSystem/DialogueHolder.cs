using System.Collections;
using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] DSDialogueContainerSO dialogue;
    
    public DSDialogueContainerSO GetDialogue()
    {
        return dialogue;
    }
}
