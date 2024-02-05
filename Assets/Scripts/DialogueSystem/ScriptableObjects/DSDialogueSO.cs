using System.Collections;
using System.Collections.Generic;
using DS.Enumerations;
using UnityEngine;

namespace DS.ScriptableObjects
{
    using Data;
    public class DSDialogueSO : ScriptableObject
    {
        public string DialogueName;
        [TextArea] public string Text;
        public List<DSDialogueChoiceData> Choices;
        public DSDialogueType DialogueType;
        public bool IsStartingDialogue;

        public void Initialize(string dialogueName, string text, List<DSDialogueChoiceData> choices, DSDialogueType dialogueType,
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
