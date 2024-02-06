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
    }
}
