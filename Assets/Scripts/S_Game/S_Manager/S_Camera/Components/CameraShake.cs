using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Knight.Camera
{
    public class CameraShake : SingletonObject<CameraShake>
    {
        private CinemachineBasicMultiChannelPerlin camShake;
        private float shakeDuration;

        public void Initialize(CinemachineBasicMultiChannelPerlin camShake)
        {
            this.camShake = camShake;
        }
        
        public void Shake(float shakeDuration, float shakeAmount, float frequency)
        {
            this.shakeDuration = shakeDuration;
            camShake.m_AmplitudeGain = shakeAmount;
            camShake.m_FrequencyGain = frequency;
        }

        public void StopShaking()
        {
            camShake.m_AmplitudeGain = 0;
            camShake.m_FrequencyGain = 0;
        }
        
        void Update() 
        {
            if (shakeDuration > 0)
            {
                shakeDuration -= Time.deltaTime;
                if (shakeDuration <= 0)
                {
                    StopShaking();
                }
            }
        }
    }
}
