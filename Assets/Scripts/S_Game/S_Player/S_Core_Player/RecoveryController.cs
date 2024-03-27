using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Health))]
public class RecoveryController : CoreComponent
{
    HitData hitData;

    Health health;
    Combat combat;
    
    private bool isInvulnerable = false;
    float hitTime;
    bool canRecover = true;

    public void SetHitData(HitData hitData)
    {
        this.hitData = hitData;
    }

    protected override void Awake() 
    {
        base.Awake();
    }

    void Start() 
    {
        health = core.GetCoreComponent<Health>();
        combat = core.GetCoreComponent<Combat>();
        AddHealthEvent();
    }

    void AddHealthEvent()
    {
        health.OnTakeDamage += TakeDamage;
        health.OnDie += Die;
    }

    void RemoveHealthEvent()
    {
        health.OnTakeDamage -= TakeDamage;
        health.OnDie -= Die;
    }

    private void OnDisable() {
        RemoveHealthEvent();
    }
    
    void Update() 
    {
        if (hitData == null) return;

        Recovering();
    }
    
    void Recovering()
    {
        if (!IsInInvulnerabilityTime() || !canRecover) return;

        if (hitTime + hitData.invulnerableTime < Time.time)
        {
            isInvulnerable = false;
            combat.EnableCollider();
        }
    }
    
    void TakeDamage(bool needToResetPlayerPosition)
    {
        hitTime = Time.time;
        isInvulnerable = true;
        
        if (needToResetPlayerPosition) hitTime += hitData.resetGroundPlayerPositionTime;

        combat.DisableCollider();
    }

    private void Die()
    {
        canRecover = false;

        combat.DisableCollider();
    }
    
    public bool IsInInvulnerabilityTime()
    {
        return isInvulnerable;
    }
}
