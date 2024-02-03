using System.Collections;
using System.Collections.Generic;
using DS.Enumerations;
using UnityEngine;

namespace DS.ScriptableObjects
{
    public class DSDialogueSO : ScriptableObject
    {
        public string DialogueName;
        [TextArea] public string Text;
        public List<string> Choices;
        public DSDialogueType DialogueType;
        public bool IsStartingDialogue;

        public DSDialogueSO(string dialogueName, string text, List<string> choices, DSDialogueType dialogueType,
            bool isStartingDialogue)
        {
            DialogueName = dialogueName;
            Text = text;
            Choices = choices;
            DialogueType = dialogueType;
            IsStartingDialogue = isStartingDialogue;
        }
    }
    
}
