using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/Data/Quest/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirement")]
    public int levelRequirement;

    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public Reward[] rewards;
    
    [Serializable]
    public class Reward
    {
        public ItemData itemData;
        public int      number;
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
