using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoPoint;

    private bool       playerIsNear = false;
    private string     questId;
    private QuestState currentQuestState;

    private void Awake()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
