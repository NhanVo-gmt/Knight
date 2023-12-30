using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : CoreComponent
{
    [Serializable]
    class ParticleRegion
    {
        public SceneLoader.Region region;
        public ParticleSystem runParticle;
        public ParticleSystem[] particleSystems;
    }

    [SerializeField] private List<ParticleRegion> particleList = new List<ParticleRegion>();
    private ParticleSystem currentRunParticle;
    
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
                if (particle.runParticle != null)
                {
                    currentRunParticle = particle.runParticle;
                    currentRunParticle.gameObject.SetActive(true);
                }
                
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
        if (currentRunParticle == null) return;

        if (!isActive)
        {
            currentRunParticle.Stop();
        } else currentRunParticle.Play();
    }
}
