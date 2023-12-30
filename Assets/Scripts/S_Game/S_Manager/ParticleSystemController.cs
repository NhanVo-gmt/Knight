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
        public ParticleSystem[] particleSystems;
    }

    [SerializeField] private List<ParticleRegion> particleList = new List<ParticleRegion>();
    [SerializeField] private GameObject runParticle;
    [SerializeField] private GameObject envParticle;
    
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
                foreach (ParticleSystem singleParticle in particle.particleSystems)
                {
                    singleParticle.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (ParticleSystem singleParticle in particle.particleSystems)
                {
                    singleParticle.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetRunParticle(bool isActive)
    {
        runParticle.SetActive(isActive);
    }
}
