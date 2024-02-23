using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ParticleSystemController : CoreComponent
{
    [Serializable]
    class ParticleRegion
    {
        public SceneLoaderEnum.Region region;
        public ParticleSystem runParticle;
        public ParticleSystem landParticle;
        public ParticleSystem[] particleSystems;
        public bool hasLight;

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
    [Header("Player")]
    [SerializeField] private List<ParticleRegion> particleList = new List<ParticleRegion>();
    private ParticleRegion currentParticleRegion;

    [SerializeField] private ParticleSystem restParticle;
    [SerializeField] private ParticleSystem[] hitParticles;

    [Header("Enemy")] 
    [SerializeField] private ParticleSystem[] enemyHitParticles;

    [Header("Light")] [SerializeField] private GameObject light2D;
    
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
                light2D.SetActive(currentParticleRegion.hasLight);
            }
            else
            {
                particle.SetActive(false);
            }
        }
    }

    #region Play Particle

    public void PlayRunParticle(bool isActive)
    {
        if (currentParticleRegion == null || currentParticleRegion.runParticle == null) return;

        if (!isActive)
        {
            currentParticleRegion.runParticle.Stop();
        } else currentParticleRegion.runParticle.Play();
    }
    
    public void PlaylandParticle(bool isActive)
    {
        if (currentParticleRegion == null || currentParticleRegion.landParticle == null) return;

        if (!isActive)
        {
            currentParticleRegion.landParticle.Stop();
        } else currentParticleRegion.landParticle.Play();
    }

    public void PlayRestParticle()
    {
        restParticle.Play();
    }

    private readonly float hitParticleMaxRotation = 10f;
    public void PlayHitParticle()
    {
        float rotateX = Random.Range(-hitParticleMaxRotation, hitParticleMaxRotation);
        
        foreach (ParticleSystem particle in hitParticles)
        {
            particle.transform.eulerAngles = new Vector3(rotateX, particle.transform.eulerAngles.y, 0);
            particle.Play();

            rotateX *= -1;
        }
    }

    public void PlayEnemyHitParticle()
    {
        foreach (ParticleSystem particle in enemyHitParticles)
        {
            particle.Play();
        }
    }
    
    #endregion

}
