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
        
        private AudioSource audioSource;
        private SceneLoader sceneLoader;

        protected override void Awake()
        {
            base.Awake();
            
            audioSource = GetComponent<AudioSource>();
            sceneLoader = GetComponent<SceneLoader>();
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
    }
}
