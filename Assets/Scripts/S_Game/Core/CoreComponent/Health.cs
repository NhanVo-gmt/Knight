using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : CoreComponent
{
    [SerializeField] int health; //todo set private

    public Action onTakeDamage;
    public Action onDie;
    public Action<int> onUpdateHealth;

    

    private bool isDie = false;

    #region Set up
    
    public void SetHealth(HealthData data)
    {
        health = data.health;
        onUpdateHealth?.Invoke(health);
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

        onUpdateHealth?.Invoke(health);

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
        onTakeDamage?.Invoke();
    }

    private void Die()
    {
        isDie = true;
        onDie?.Invoke();
    }
}
