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
    }
}
