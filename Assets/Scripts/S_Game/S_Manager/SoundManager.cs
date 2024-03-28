using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }
        
        [SerializeField] private List<RegionClip> regionClips;
        
        private SceneLoader sceneLoader;
        
        [SerializeField] private AudioSource backgroundAudioSource;
        [SerializeField] private AudioSource environmentAudioSource;

        protected override void Awake()
        {
            base.Awake();
            
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
                    backgroundAudioSource.clip = regionClip.clips[Random.Range(0, regionClip.clips.Count)];
                    backgroundAudioSource.Play();
                    return;
                }
            }
        }

        public void PlayOneShot(AudioClip clip)
        {
            backgroundAudioSource.PlayOneShot(clip);
        }

        #region Environment Sound
        
        [Header("Sound Effect")] 
        public AudioClip grassHitClip;
        
        public void PlayGrassHitClip()
        {
            PlayOneShot(grassHitClip);
        }
        

        #endregion
    }
}
