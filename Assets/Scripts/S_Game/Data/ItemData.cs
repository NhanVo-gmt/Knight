using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/PooledObjectData/ItemData", fileName = "ItemData")]
public class ItemData : PooledObjectData
{
    [Header("Item")]
    public string id;

    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;

    public void AssignId(string id)
    {
        this.id = id;
    }
}
