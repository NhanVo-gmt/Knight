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
        public ParticleSystem particleSystem;
    }

    [SerializeField] private List<ParticleRegion> particleList = new List<ParticleRegion>();
    
    private Transform player;

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => SceneLoader.Instance != null);
        SceneLoader.Instance.OnChangedRegion += ChangeParticleSystem;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void ChangeParticleSystem(object sender, SceneLoader.Region region)
    {
        foreach (ParticleRegion particle in particleList)
        {
            if (particle.region == region)
            {
                particle.particleSystem.gameObject.SetActive(true);
            }
        }
    }

    void TurnOffAllParticles()
    {
        foreach (ParticleRegion particle in particleList)
        {
            particle.particleSystem.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (player == null) return;
        
        transform.position = player.position;
    }
}
