using System.Collections;
using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using System;

public class DialogueHolder : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private DSDialogueContainerSO dialogue;
    
    [Header("Quest")]
    [SerializeField] private QuestInfoSO           questInfo;

    [SerializeField] private bool startPoint;
    [SerializeField] private bool finishPoint;

    private string     questId;
    private QuestState currentQuestState;
    
    public Action OnFinishDialogue;

    private void Awake()
    {
        questId = questInfo.Id;
    }

    private void OnEnable()
    {
        QuestManager.Instance.QuestEvent.OnQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        if (QuitUtils.isQuitting) return;
        
        QuestManager.Instance.QuestEvent.OnQuestStateChange -= QuestStateChange;
    }

    #region Dialogue

    public DSDialogueContainerSO GetDialogue()
    {
        return dialogue;
    }

    public void EndConversation()
    {
        OnFinishDialogue?.Invoke();
    }

    #endregion


    #region Quest

    public void StartOrContinueQuest()
    {
        if (startPoint)
        {
            QuestManager.Instance.QuestEvent.StartOrContinueQuest(questInfo.Id);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.Id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }

    #endregion
}
