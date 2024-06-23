using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuestManager : SingletonObject<QuestManager>
{
    private const string QUEST_PATH = "Assets/ScriptableObjects/Quest";
    
    [SerializeField] private List<QuestInfoSO> allQuests;

    #if UNITY_EDITOR
    [ContextMenu("Load All Quest")]
    public void LoadAllQuests()
    {
        allQuests = EditorHelperMethods.FindAssets<QuestInfoSO>(QUEST_PATH);
    }
    
    #endif
    
    private Dictionary<string, Quest> questMap;
    public  QuestEvent                QuestEvent { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        QuestEvent = new QuestEvent();
        questMap   = CreateQuestMap();
        
        Quest quest = GetQuestById("CollectCoinsQuest");
    }


    private void OnEnable()
    {
        QuestEvent.OnStartQuest   += StartQuest;
        QuestEvent.OnAdvanceQuest += AdvanceQuest;
        QuestEvent.OnFinishQuest  += FinishQuest;
    }

    private void OnDisable()
    {
        QuestEvent.OnStartQuest   -= StartQuest;
        QuestEvent.OnAdvanceQuest -= AdvanceQuest;
        QuestEvent.OnFinishQuest  -= FinishQuest;
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            QuestEvent.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        QuestEvent.QuestStateChange(quest);
    }

    private bool CheckRequirementMet(Quest quest)
    {
        bool meetRequirement = true;

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.Id).state != QuestState.FINISHED)
            {
                meetRequirement = false;
            }
        }

        return meetRequirement;
    }

    private void StartQuest(string id)
    {
        Debug.Log($"Start Quest: {id}");
    }

    private void AdvanceQuest(string id)
    {
        Debug.Log($"Advance Quest: {id}");
    }

    private void FinishQuest(string id)
    {
        Debug.Log($"Finish Quest: {id}");
    }


    private Dictionary<string, Quest> CreateQuestMap()
    {
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.Id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.Id);
            }
            idToQuestMap.Add(questInfo.Id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }

        return quest;
    }
}
