using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : SingletonObject<ItemDatabase>
{
    [SerializeField] private ItemDatabaseData itemData;
}
