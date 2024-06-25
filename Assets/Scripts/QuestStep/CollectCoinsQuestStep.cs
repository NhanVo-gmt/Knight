using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Inventory;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    [SerializeField] private ItemData coinData;
    private                  int      coinsCollected  = 0;
    private                  int      coinsToComplete = 5;

    private void OnEnable()
    {
        InventorySystem.Instance.OnChangedItem += CollectCoin;
    }

    private void OnDisable()
    {
        if (QuitUtils.isQuitting) return;
        
        InventorySystem.Instance.OnChangedItem -= CollectCoin;
    }

    private void CollectCoin(ItemData data, int number)
    {
        if (data == coinData)
        {
            coinsCollected += number;
        }

        if (coinsCollected >= coinsToComplete)
        {
            FinishQuestStep();
        }
    }
}
