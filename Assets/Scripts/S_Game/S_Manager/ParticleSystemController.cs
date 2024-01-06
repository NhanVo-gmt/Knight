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
        public SceneLoaderEnum.Region region;
        public ParticleSystem runParticle;
        public ParticleSystem landParticle;
        public ParticleSystem[] particleSystems;

        public void SetActive(bool isActive)
        {
            if (runParticle) runParticle.gameObject.SetActive(isActive);
            if (landParticle) landParticle.gameObject.SetActive(isActive);
            
            foreach (ParticleSystem singleParticle in particleSystems)
            {
                singleParticle.gameObject.SetActive(isActive);
            }
        }
    }

    [SerializeField] private List<ParticleRegion> particleList = new List<ParticleRegion>();
    private ParticleRegion currentParticleRegion;

    [SerializeField] private ParticleSystem restParticle;
    
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

    private void ChangeParticleSystem(object sender, SceneLoaderEnum.Region region)
    {
        foreach (ParticleRegion particle in particleList)
        {
            if (particle.region == region)
            {
                currentParticleRegion = particle;

                currentParticleRegion.SetActive(true);
            }
            else
            {
                particle.SetActive(false);
            }
        }
    }

    public void SetRunParticle(bool isActive)
    {
        if (currentParticleRegion == null || currentParticleRegion.runParticle == null) return;

        if (!isActive)
        {
            currentParticleRegion.runParticle.Stop();
        } else currentParticleRegion.runParticle.Play();
    }
    
    public void SetlandParticle(bool isActive)
    {
        if (currentParticleRegion == null || currentParticleRegion.landParticle == null) return;

        if (!isActive)
        {
            currentParticleRegion.landParticle.Stop();
        } else currentParticleRegion.landParticle.Play();
    }

    public void SetRestParticle()
    {
        restParticle.Play();
    }
}
