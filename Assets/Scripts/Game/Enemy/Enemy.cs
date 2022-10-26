using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData data;

    AttackData touchAttackData;

    Core core;
    Health health;
    Combat combat;
    VFXController vfx;

    Collider2D col;

    #region Set up
    
    void Awake() 
    {
        col = GetComponent<Collider2D>();
        core = GetComponentInChildren<Core>();
        touchAttackData = FindObjectOfType<GameSettings>().TouchAttackSettings;
        
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
        vfx = core.GetCoreComponent<VFXController>();

        SetUpComponent();
    }

    void SetUpComponent()
    {
        combat.SetUpDamagerType(IDamageable.DamagerType.Enemy);
        health.SetHealth(data.healthData);
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D other) {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == col) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(touchAttackData, IDamageable.DamagerType.Enemy);
        }
    }
}
