using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Knight.Manager
{
    public class SoundManager : SingletonObject<SoundManager>
    {
        [Serializable]
        public class RegionClip
        {
            public SceneLoaderEnum.Region region;
            public List<AudioClip> clips;
            public AudioClip[] moveClips;
            public AudioClip landClip;
        }
        
        [SerializeField] private List<RegionClip> regionClips;

        [SerializeField] private RegionClip currentRegionClip;
        
        private SceneLoader sceneLoader;
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();

            audioSource = GetComponent<AudioSource>();
            sceneLoader = GetComponentInParent<SceneLoader>();
        }

        private void OnEnable()
        {
            sceneLoader.OnChangedRegion += ChangeBackgroundMusic;
        }

        private void ChangeBackgroundMusic(object sender, SceneLoaderEnum.Region newRegion)
        {
            foreach (RegionClip regionClip in regionClips)
            {
                if (regionClip.region == newRegion)
                {
                    currentRegionClip = regionClip;
                    
                    audioSource.clip = regionClip.clips[Random.Range(0, regionClip.clips.Count)];
                    audioSource.Play();
                    return;
                }
            }
        }

        public void PlayOneShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        #region Environment Sound
        
        [Header("Sound Effect")] 
        public AudioClip grassHitClip;
        
        public void PlayGrassHitClip()
        {
            PlayOneShot(grassHitClip);
        }

        public void PlayLandClip()
        {
            PlayOneShot(currentRegionClip.landClip);
        }

        public void PlayMoveClip()
        {
            PlayOneShot(currentRegionClip.moveClips[Random.Range(0, currentRegionClip.moveClips.Length)]);
        }
        
        #endregion

    }
}
