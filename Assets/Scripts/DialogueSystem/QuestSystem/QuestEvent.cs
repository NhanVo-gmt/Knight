
using System;

public class QuestEvent
{
    public event Action<string> OnStartOrContinueQuest;
    public void StartOrContinueQuest(string id)
    {
        if (OnStartOrContinueQuest != null)
        {
            OnStartOrContinueQuest(id);
        }
    }
    
    public event Action<string> OnStartQuest;
    public void StartQuest(string id)
    {
        if (OnStartQuest != null)
        {
            OnStartQuest(id);
        }
    }
    
    public event Action<string> OnAdvanceQuest;
    public void AdvanceQuest(string id)
    {
        if (OnAdvanceQuest != null)
        {
            OnAdvanceQuest(id);
        }
    }
    
    public event Action<string> OnFinishQuest;
    public void FinishQuest(string id)
    {
        if (OnFinishQuest != null)
        {
            OnFinishQuest(id);
        }
    }
    
    public event Action<Quest> OnQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (OnQuestStateChange != null)
        {
            OnQuestStateChange(quest);
        }
    }

    public event Action<QuestInfoSO.Reward[]> OnQuestClaimRewards;
    public void ClaimRewardQuest(Quest quest)
    {
        if (OnQuestClaimRewards != null)
        {
            OnQuestClaimRewards(quest.info.rewards);
        }
    }
}
