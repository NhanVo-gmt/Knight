using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemData : ScriptableObject
{
    [Serializable]
    public class ShopSingleItemData
    {
        public ItemData ItemData;
        public int      Number;
        public int      Price;
    }

    public List<ShopSingleItemData> ItemDatas = new();

    public void Initialize()
    {
        
    }

    public void Save()
    {
        //todo
    }

    public void Load()
    {
        //todo
    }
}
