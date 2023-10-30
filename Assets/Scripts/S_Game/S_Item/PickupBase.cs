using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            PickUp();
        }
    }

    protected virtual void PickUp()
    {
        gameObject.GetComponent<PooledObject>().Release();
    }
}
