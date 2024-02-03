using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS.ScriptableObjects
{
    public class DSDialogueGroupSO : ScriptableObject
    {
        public string GroupName;

        public void Initialize(string groupName)
        {
            GroupName = groupName;
        }
    }
    
}
