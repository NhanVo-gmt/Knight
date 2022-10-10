using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : CoreComponent
{
    [SerializeField] HealthData healthData; //todo set private
    HitData hitData;

    public Action onTakeDamageAction;
    public Action onDieAction;


    float hitTime;

    #region Set up
    
    public void SetHealth(HealthData data)
    {
        this.healthData = Instantiate(data);
    }

    public void SetHitData(HitData hitData)
    {
        this.hitData = hitData;
    }

    #endregion
    
    protected override void Awake() 
    {
        base.Awake();
    }
    
    public void TakeDamage(AttackData attackData)
    {
        if (healthData.health <= 0 || IsInInvulnerabiltyTime()) return;

        healthData.health -= attackData.damage;

        if (healthData.health <= 0)
        {
            Die();
        }
        else
        {
            onTakeDamageAction?.Invoke();
            hitTime = Time.time;
        }
    }

    public bool IsInInvulnerabiltyTime()
    {
        if (hitData == null) return false;

        return hitTime + hitData.invulnerableTime < Time.time;
    }

    private void Die()
    {
        onDieAction?.Invoke();
    }
}
