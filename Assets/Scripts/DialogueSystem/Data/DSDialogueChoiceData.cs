using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS.Data
{
    using ScriptableObjects;
    
    [Serializable]
    public class DSDialogueChoiceData
    {
        public string Text;
        public DSDialogueSO NextDialogue;
    }
}
