using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData data;

    Core core;
    Health health;
    Combat combat;

    Collider2D col;

    #region Set up
    
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
        combat = core.GetCoreComponent<Combat>();

        SetUpComponent();
    }

    void SetUpComponent()
    {
        combat.SetUpDamagerType(IDamageable.DamagerType.Enemy);
        health.SetHealth(data.healthData);
    }

    #endregion


    private void OnCollisionStay2D(Collision2D other) {
        if (other.collider == col) return;

        if (other.collider.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(data.touchAttackData, IDamageable.DamagerType.Enemy);
        }
    }
}
