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
        public List<string> GetGroupedDialogueNames(DSDialogueGroupSO dialogueGroup, bool startingDialoguesOnly = false)
        {
            
            List<DSDialogueSO> groupedDialogues = DialogueGroups[dialogueGroup];
            List<string> groupedDialogueNames = new List<string>();
            foreach (DSDialogueSO groupedDialogue in groupedDialogues)
            {
                if (startingDialoguesOnly && !groupedDialogue.IsStartingDialogue)
                {
                    continue;
                }
                groupedDialogueNames.Add(groupedDialogue.DialogueName);
            }

            return groupedDialogueNames;
        }

        public List<string> GetUngroupedDialogueNames(bool startingDialoguesOnly = false)
        {
            List<string> ungroupedDialogueNames = new List<string>();
            foreach (DSDialogueSO ungroupedDialogue in UngroupedDialogues)
            {
                if (startingDialoguesOnly && !ungroupedDialogue.IsStartingDialogue)
                {
                    continue;
                }
                ungroupedDialogueNames.Add(ungroupedDialogue.DialogueName);
            }

            return ungroupedDialogueNames;
        }
    }
}
