using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class Health : CoreComponent
{
    [SerializeField] int health; //todo set private

    public Action OnTakeDamage;
    public Action OnDie;
    public Action<int> OnUpdateHealth;

    

    private bool isDie = false;

    #region Set up
    
    public void SetHealth(HealthData data)
    {
        health = data.health;
        OnUpdateHealth?.Invoke(health);
    }

    #endregion
    
    protected override void Awake() 
    {
        base.Awake();
    }
    
    public void TakeDamage(int damage)
    {
        if (health <= 0 || IsInvulnerable()) return;

        health -= damage;

        OnUpdateHealth?.Invoke(health);

        if (health > 0)
        {
            TakeDamage();
        }
        else
        {
            Die();
        }
    }

    protected virtual bool IsInvulnerable()
    {
        return false;
    }

    void TakeDamage()
    {
        OnTakeDamage?.Invoke();
    }

    private void Die()
    {
        isDie = true;
        OnDie?.Invoke();
    }
}
