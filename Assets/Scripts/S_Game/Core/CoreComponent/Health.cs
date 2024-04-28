using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class Health : CoreComponent
{
    [SerializeField] int health; //todo set private
    private int maxHealth;

    public Action<bool> OnTakeDamage;
    public Action OnDie;
    public Action<int> OnUpdateHealth;

    public Action OnResetGroundPosition;

    public bool isDie = false;

    #region Set up
    
    public void SetHealth(HealthData data)
    {
        maxHealth = data.health;
        health = maxHealth;
        OnUpdateHealth?.Invoke(health);
    }

    #endregion
    
    protected override void Awake() 
    {
        base.Awake();
    }

    public bool TakeDamage(int damage)
    {
        return TakeDamage(damage, false);
    }
    
    public bool TakeDamage(int damage, bool needToResetPlayerPosition)
    {
        if (health <= 0 || IsInvulnerable()) return false;

        health -= damage;

        OnUpdateHealth?.Invoke(health);

        if (health > 0)
        {
            TakeDamage(needToResetPlayerPosition);
        }
        else
        {
            Die();
        }

        return true;
    }

    protected virtual bool IsInvulnerable()
    {
        return false;
    }

    void TakeDamage(bool needToResetPlayerPosition)
    {
        OnTakeDamage?.Invoke(needToResetPlayerPosition);
    }

    private void Die()
    {
        isDie = true;
        OnDie?.Invoke();
    }

    public int GetPercent()
    {
        return Mathf.RoundToInt(health * 1.0f / maxHealth * 100) ;
    }
}
