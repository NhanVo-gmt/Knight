using System.Collections;
using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using System;
using System.Linq;

public class DialogueHolder : MonoBehaviour, IDataPersistence
{
    [Header("NPC")]
    [SerializeField] private string id;
    
    [Header("Dialogue")]
    [SerializeField] private DSDialogueContainerSO dialogue;
    
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfo;

    [Header("Shop")]
    [SerializeField] private ShopItemData shopItemData;

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

    #region Shop

    public ShopItemData GetShopData()
    {
        return shopItemData;
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

    public bool IsLoadFirstTime()
    {
        return false;
    }
    public void LoadData(GameData gameData)
    {
        if (gameData.npcDict.TryGetValue(id, out GameData.NPCSaveData npcSaveData))
        {
            shopItemData.ItemDictionary =
                new(npcSaveData.ShopSaveData.ToDictionary(x => GameSettings.Instance.ItemDatabaseData.GetItem(x.Key),
                    y => y.Value));
        }
    }
    
    public void SaveData(ref GameData data)
    {
        if (!data.npcDict.ContainsKey(id))
        {
            data.npcDict.Add(id, new(id));
        }

        data.npcDict[id].ShopSaveData = new(shopItemData.ItemDictionary.ToDictionary(x => x.Key.id, y => y.Value));
    }
}
