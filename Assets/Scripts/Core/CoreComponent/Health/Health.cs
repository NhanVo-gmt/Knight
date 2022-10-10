using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : CoreComponent
{
    [SerializeField] HealthData data; //todo set private

    public Action onTakeDamageAction;
    public Action onDieAction;

    #region Set up
    
    public void SetHealth(HealthData data)
    {
        this.data = Instantiate(data);
    }

    #endregion
    
    protected override void Awake() 
    {
        base.Awake();
    }
    
    public void TakeDamage(AttackData attackData)
    {
        if (data.health <= 0) return;

        data.health -= attackData.damage;

        if (data.health <= 0)
        {
            Die();
        }
        else
        {
            onTakeDamageAction?.Invoke();
        }
    }

    private void Die()
    {
        onDieAction?.Invoke();
    }
}
