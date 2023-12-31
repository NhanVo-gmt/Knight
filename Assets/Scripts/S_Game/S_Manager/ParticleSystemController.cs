using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleSystemController : CoreComponent
{
    [Serializable]
    class ParticleRegion
    {
        public SceneLoader.Region region;
        public ParticleSystem runParticle;
        public ParticleSystem landParticle;
        public ParticleSystem[] particleSystems;
    }

    [SerializeField] private List<ParticleRegion> particleList = new List<ParticleRegion>();
    private ParticleRegion currentParticleRegion;
    
    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => SceneLoader.Instance != null);
        SceneLoader.Instance.OnChangedRegion += ChangeParticleSystem;
        
        ChangeParticleSystem(this, SceneLoader.Instance.GetCurrentRegion());
    }

    private void ChangeParticleSystem(object sender, SceneLoader.Region region)
    {
        foreach (ParticleRegion particle in particleList)
        {
            if (particle.region == region)
            {
                currentParticleRegion = particle;
                
                foreach (ParticleSystem singleParticle in particle.particleSystems)
                {
                    singleParticle.gameObject.SetActive(true);
                }
            }
            else
            {
                if (particle.runParticle != null)
                    particle.runParticle.gameObject.SetActive(false);
                
                foreach (ParticleSystem singleParticle in particle.particleSystems)
                {
                    singleParticle.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetRunParticle(bool isActive)
    {
        if (currentParticleRegion == null) return;

        if (!isActive)
        {
            currentParticleRegion.runParticle.Stop();
        } else currentParticleRegion.runParticle.Play();
    }
    
    public void SetlandParticle(bool isActive)
    {
        if (currentParticleRegion == null) return;

        if (!isActive)
        {
            currentParticleRegion.landParticle.Stop();
        } else currentParticleRegion.landParticle.Play();
    }
}
