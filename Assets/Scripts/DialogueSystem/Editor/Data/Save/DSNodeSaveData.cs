using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS.Data.Save
{
    using Enumerations;
    
    [Serializable]
    public class DSNodeSaveData
    {
        public string                 ID;
        public string                 Name;
        public string                 Text;
        public List<DSChoiceSaveData> Choices;
        public string                 GroupID;
        public DSDialogueType         DialogueType;
        public Vector2                Position;
        public string                 ShopItemPath;
        public string                 QuestInfoPath;
    }
}
