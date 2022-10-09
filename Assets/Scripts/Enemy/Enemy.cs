using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData data;


    Core core;
    Health health;

    Collider2D col;

    void Awake() 
    {
        col = GetComponent<Collider2D>();

        core = GetComponentInChildren<Core>();
        
        data = Instantiate(data);
    }

    void Start() 
    {
        GetCoreComponent();
    }

    private void GetCoreComponent()
    {
        health = core.GetCoreComponent<Health>();
        health.SetHealth(data.healthData);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider == col) return;


        if (other.collider.TryGetComponent<Combat>(out Combat combat))
        {
            
        }
    }
}
