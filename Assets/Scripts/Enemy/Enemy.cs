using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData data;

    Collider2D col;

    void Awake() 
    {
        col = GetComponent<Collider2D>();
        
        data = Instantiate(data);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider == col) return;


        if (other.collider.TryGetComponent<Combat>(out Combat combat))
        {
            
        }
    }
}
