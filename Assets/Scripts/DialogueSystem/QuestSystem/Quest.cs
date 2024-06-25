using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Quest
{
    public QuestInfoSO info;
    [SerializeField][ReadOnlyInspector] private List<QuestStep> currentQuestStep = new();

    public  QuestState state;
    private int        currentQuestStepIndex;


    public Quest(QuestInfoSO questInfo)
    {
        this.info                  = questInfo;
        this.state                 = QuestState.REQUIREMENT_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < info.questStepPrefabs.Length;
    }

    public bool IsFinishedCurrentStep()
    {
        return currentQuestStep.Count == 0;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            // todo object pooling
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.Id);
            questStep.OnFinish += OnQuestStepFinish;
            
            currentQuestStep.Add(questStep);
        }

        void OnQuestStepFinish(QuestStep questStep)
        {
            currentQuestStep.Remove(questStep);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogError("Tried to get quest step prefab, but stepIndex was out of range indicating that " +
                "there's no current step: QuestId=" + info.Id + ", stepIndex=" + currentQuestStepIndex);
        }

        return questStepPrefab;
    }
}
