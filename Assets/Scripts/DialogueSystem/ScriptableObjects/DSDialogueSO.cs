using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DS.Enumerations;
using UnityEngine;

namespace DS.ScriptableObjects
{
    using Data;
    public class DSDialogueSO : ScriptableObject
    {
        public string DialogueName;
        [TextArea] public string Text;
        public DSDialogueType DialogueType;
        public bool IsStartingDialogue;
        
        public List<DSDialogueChoiceData> Choices;
        public ShopItemData               ShopItem;

        public void Initialize(string dialogueName, string text, List<DSDialogueChoiceData> choices, DSDialogueType dialogueType,
            bool isStartingDialogue)
        {
            DialogueName = dialogueName;
            Text = text;
            Choices = choices;
            DialogueType = dialogueType;
            IsStartingDialogue = isStartingDialogue;
        }

        public DSDialogueSO GetNextDialogueByIndex(int choice)
        {
            return Choices[choice].NextDialogue;
        }
        
        public DSDialogueSO GetNextDialogueByString(string choice)
        {
            return Choices.Where(x => x.Text == choice).FirstOrDefault()?.NextDialogue;
        } 
        
    }
    
}
