using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Inventory;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int number = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            PickUp();
        }
    }

    protected virtual void PickUp()
    {
        InventorySystem.Instance.AddItem(itemData, number);
        gameObject.GetComponent<PooledObject>().Release();
    }
}
