using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    
    private Collider2D col;
    private Core core;
    private Health health;
    private Combat combat;

    void Awake()
    {
        core = GetComponentInChildren<Core>();
    }

    void Start() 
    {
        combat = core.GetCoreComponent<Combat>();
        health = core.GetCoreComponent<Health>();
        SetupComponent();

        health.OnTakeDamage += PlayHitClip;
        health.OnDie += Die;
    }

    void SetupComponent()
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Enemy, data.KnockbackType, data.hasShield);
        health.SetHealth(data.healthData); 
    }

    private void Die()
    {
        PlayHitClip();
        
        for (int i = 0; i < data.numberOfSoulDropped; i++)
        {
            ObjectPoolManager.Instance.SpawnPooledPrefab(GameSettings.Instance.soul, transform.position, Vector2.left);
        }
        gameObject.SetActive(false);
    }

    #region Play Sound
    
    private void PlayHitClip(bool obj = false)
    {
        SoundManager.Instance.PlayOneShot(data.GetRandomHitClip());
    }

    #endregion
}
