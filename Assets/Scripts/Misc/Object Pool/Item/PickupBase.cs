using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupBase : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private readonly float flyForce = 3f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Init(ItemData itemData)
    {
        this.itemData = itemData;
        sprite.sprite = itemData.sprite;
    }

    private void OnEnable()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
        rb.AddForce(direction * flyForce, ForceMode2D.Impulse);
    }

    public ItemData GetItem()
    {
        return itemData;
    }
    
    public virtual void Release()
    {
        gameObject.GetComponent<PooledObject>().Release();
    }
}
