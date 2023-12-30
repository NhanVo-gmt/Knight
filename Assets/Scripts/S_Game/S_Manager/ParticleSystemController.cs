using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [Serializable]
    class ParticleRegion
    {
        public SceneLoader.Region region;
        public ParticleSystem[] particleSystems;
    }

    [SerializeField] private List<ParticleRegion> particleList = new List<ParticleRegion>();
    
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
                foreach (ParticleSystem particleSystem in particle.particleSystems)
                {
                    particleSystem.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (ParticleSystem particleSystem in particle.particleSystems)
                {
                    particleSystem.gameObject.SetActive(false);
                }
            }
        }
    }
}
