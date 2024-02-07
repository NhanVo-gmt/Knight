using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.ScriptableObjects
{
    [Serializable]
    public class DSDialogueContainerSO : ScriptableObject
    {
        public string FileName;
        public SerializableDictionary<DSDialogueGroupSO, List<DSDialogueSO>> DialogueGroups;
        public List<DSDialogueSO> UngroupedDialogues;

        public void Initialize(string fileName)
        {
            FileName = fileName;
            DialogueGroups = new SerializableDictionary<DSDialogueGroupSO, List<DSDialogueSO>>();
            UngroupedDialogues = new List<DSDialogueSO>();
        }

        public List<string> GetDialogueGroupNames()
        {
            List<string> dialogueGroupNames = new List<string>();
            foreach (DSDialogueGroupSO dialogueGroup in DialogueGroups.Keys)
            {
                dialogueGroupNames.Add(dialogueGroup.GroupName);
            }

            return dialogueGroupNames;
        }

        // Get name of dialogue in groups
        public List<string> GetGroupedDialogueNames(DSDialogueGroupSO dialogueGroup)
        {
            List<DSDialogueSO> groupedDialogues = DialogueGroups[dialogueGroup];
            List<string> groupedDialogueNames = new List<string>();
            foreach (DSDialogueSO groupedDialogue in groupedDialogues)
            {
                groupedDialogueNames.Add(groupedDialogue.DialogueName);
            }

            return groupedDialogueNames;
        }

        public List<string> GetUngroupedDialogueNames()
        {
            List<string> ungroupedDialogueNames = new List<string>();
            foreach (DSDialogueSO ungroupedDialogue in UngroupedDialogues)
            {
                ungroupedDialogueNames.Add(ungroupedDialogue.DialogueName);
            }

            return ungroupedDialogueNames;
        }
    }
}
