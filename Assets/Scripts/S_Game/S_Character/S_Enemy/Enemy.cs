using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    
    private BehaviourTreeRunner runner;
    
    [Header("Core component")]
    private Core               core;
    private Health             health;
    private Combat             combat;
    private AnimatorController animator;
    private Movement           movement;

    void Awake()
    {
        runner = GetComponent<BehaviourTreeRunner>();
        
        core     = GetComponentInChildren<Core>();
        combat   = core.GetCoreComponent<Combat>();
        health   = core.GetCoreComponent<Health>();
        movement = core.GetCoreComponent<Movement>();
        animator = core.GetCoreComponent<AnimatorController>();
        SetupComponent();
    }

    void OnEnable() 
    {
        health.OnTakeDamage += PlayHitClip;
        health.OnDie += Die;
        animator.OnDie += TurnOffObject;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= PlayHitClip;
        health.OnDie        -= Die;
        animator.OnDie      -= TurnOffObject;
    }

    void SetupComponent()
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Enemy, data.KnockbackType, data.hasShield);
        health.SetHealth(data.healthData); 
    }

    private void Die()
    {
        runner.enabled = false;
        
        PlayHitClip();
        PlayParticle();
        
        for (int i = 0; i < data.numberOfSoulDropped; i++)
        {
            ObjectPoolManager.Instance.SpawnPooledPrefab(GameSettings.Instance.soul, transform.position, Vector2.left);
        }
        
    }
    private void PlayParticle()
    {
        ObjectPoolManager.Instance.SpawnPooledPrefab(GameSettings.Instance.deathParticle, transform.position, movement.faceDirection);
    }

    private void TurnOffObject()
    {
        gameObject.SetActive(false);
    }

    #region Play Sound
    
    private void PlayHitClip(bool obj = false)
    {
        SoundManager.Instance.PlayOneShot(data.GetRandomHitClip());
    }

    #endregion
}
