using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : CoreComponent
{
    [SerializeField] HealthData healthData; //todo set private
    HitData hitData;

    public Action onRecover;
    public Action onTakeDamage;
    public Action onDie;

    private bool isDie = false;
    private bool isInvulnerable;
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

    void Update() 
    {
        Recover();
    }

    void Recover()
    {
        if (!IsInInvulnerabiltyTime() || isDie) return;

        if (hitTime + hitData.invulnerableTime < Time.time)
        {
            isInvulnerable = false;
            onRecover?.Invoke();
        }
    }
    
    public void TakeDamage(AttackData attackData)
    {
        if (healthData.health <= 0 || isInvulnerable) return;

        healthData.health -= attackData.damage;

        if (healthData.health > 0)
        {
            TakeDamage();
        }
        else
        {
            Die();
        }
    }

    void TakeDamage()
    {
        hitTime = Time.time;
        isInvulnerable = true;
        onTakeDamage?.Invoke();
    }

    private void Die()
    {
        isDie = true;
        onDie?.Invoke();
    }

    public bool IsInInvulnerabiltyTime()
    {
        return isInvulnerable;
    }
}
