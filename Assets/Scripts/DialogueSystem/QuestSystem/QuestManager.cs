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
    
    private Dictionary<string, Quest> questMap = new();
    public  QuestEvent                QuestEvent { get; private set; } = new();

    protected override void Awake()
    {
        questMap   = CreateQuestMap();
        
        base.Awake();
    }


    private void OnEnable()
    {
        QuestEvent.OnStartOrContinueQuest += StartOrContinueQuest;
        QuestEvent.OnStartQuest           += StartQuest;
        QuestEvent.OnAdvanceQuest         += AdvanceQuest;
        QuestEvent.OnFinishQuest          += FinishQuest;
    }

    private void OnDisable()
    {
        QuestEvent.OnStartOrContinueQuest -= StartOrContinueQuest;
        QuestEvent.OnStartQuest           -= StartQuest;
        QuestEvent.OnAdvanceQuest         -= AdvanceQuest;
        QuestEvent.OnFinishQuest          -= FinishQuest;
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

    private void StartOrContinueQuest(string id)
    {
        Quest quest = GetQuestById(id);
        
        Debug.Log($"{quest.info.displayName}: {quest.state}");
        
        switch (quest.state)
        {
            case QuestState.REQUIREMENT_NOT_MET:
                if (CheckRequirementMet(quest))
                {
                    StartQuest(quest);
                }
                break;
            case QuestState.CAN_START:
                StartQuest(quest);
                break;
            case QuestState.IN_PROGRESS:
                if (quest.IsFinishedCurrentStep())
                {
                    AdvanceQuest(quest);
                }
                break;
            case QuestState.CAN_FINISH:
                FinishQuest(quest);
                break;
            case QuestState.FINISHED:
                break;
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        StartQuest(quest);
    }
    
    private void StartQuest(Quest quest)
    {
        Debug.Log($"Start Quest: {quest.info.displayName}");
        
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.Id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        
        AdvanceQuest(quest);
    }
    
    private void AdvanceQuest(Quest quest)
    {
        Debug.Log($"Advance Quest: {quest.info.displayName}");
        
        quest.MoveToNextStep();
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.Id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        
        FinishQuest(quest);
    }

    private void FinishQuest(Quest quest)
    {
        Debug.Log($"Finish Quest: {quest.info.displayName}");
        ChangeQuestState(quest.info.Id, QuestState.FINISHED);

        ClaimRewards(quest);
    }
    
    private void ClaimRewards(Quest quest)
    {
        Debug.Log($"Claim Reward Quest: {quest.info.displayName}");
        
        QuestEvent.ClaimRewardQuest(quest);
        
        ChangeQuestState(quest.info.Id, QuestState.CLAIM_REWARD);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        Dictionary<string, Quest> idToQuestMap = new();
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

    public void Save()
    {
        // todo save
    }

    public void Load()
    {
        
    }
}
