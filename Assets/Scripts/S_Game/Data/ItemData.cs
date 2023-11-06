using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/PooledObjectData/ItemData", fileName = "ItemData")]
public class ItemData : PooledObjectData
{
    [Header("Item")]
    public string id;

    public string itemName;
    public Sprite sprite;
    [TextArea(5, 10)] public string itemDescription;

    public void AssignId(string id)
    {
        this.id = id;
    }
}
