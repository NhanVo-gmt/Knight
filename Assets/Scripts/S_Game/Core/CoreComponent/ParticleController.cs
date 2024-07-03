using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : CoreComponent
{
    [SerializeField] private ParticleSystem[] deathParticles;

    private Health health;
    
    protected override void Awake()
    {
        base.Awake();
        
        health = core.GetCoreComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDie += PlayDeathParticle;
    }

    private void OnDisable()
    {
        health.OnDie -= PlayDeathParticle;
    }


    private void PlayDeathParticle()
    {
        foreach (ParticleSystem particleSystem in deathParticles)
        {
            particleSystem.Play();
        }
    }
}
