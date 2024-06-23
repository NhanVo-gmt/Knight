using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint
{
    [SerializeField] [ReadOnlyInspector] private QuestInfoSO questInfoPoint;

    private bool       playerIsNear = false;
    private string     questId;
    private QuestState currentQuestState;

    private void Initalize()
    {
        questId = questInfoPoint.Id;
    }

    private void OnEnable()
    {
        QuestManager.Instance.QuestEvent.OnQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        QuestManager.Instance.QuestEvent.OnQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.Id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }
    
}
