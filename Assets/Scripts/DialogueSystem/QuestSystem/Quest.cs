using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;

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

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
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