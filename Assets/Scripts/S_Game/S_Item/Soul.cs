using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Soul : PickupBase
{
    private Rigidbody2D rb;
    private readonly float flyForce = 3f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
        rb.AddForce(direction * flyForce, ForceMode2D.Impulse);
    }
}
