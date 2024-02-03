using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS.Data.Save
{
    public class DSGraphSaveDataSO : ScriptableObject
    {
        public string FileName;
        public List<DSGroupSaveData> Groups;
        public List<DSNodeSaveData> Nodes;
        public List<string> OldGroupNames;
        public List<string> OldUngroupedNodeNames;
        public SerializableDictionary<string, List<string>> OldGroupedNodeNames;

        public void Initialize(string fileName)
        {
            FileName = fileName;

            Groups = new List<DSGroupSaveData>();
            Nodes = new List<DSNodeSaveData>();
        }
    }
    
}
