using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Knight.Camera
{
    public class CameraController : SingletonObject<CameraController>
    {
        [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

        [Header("Control for lerping for player fall/jump")] 
        [SerializeField] private float fallPanAmount = 0.25f;
        [SerializeField] private float fallYPanTime = 0.35f;
        public float fallSpeedYDampingChangeThreshold = -15f;

        public bool IsLerpingYDamping;// { get; private set; }
        public bool LerpedFromPlayerFalling;// { get; set; }

        private Coroutine lerpYPanCoroutine;
        private CinemachineVirtualCamera currentCamera;
        private CinemachineFramingTransposer framingTransposer;
        private float normYPanAmount;
        
        // Scene Cam
        public Vector2 startPos { get; private set; }

        private Transform cam;
        private CameraConfiner confiner;

        protected override void Awake() 
        {
            base.Awake();

            for (int i = 0; i < virtualCameras.Length; i++)
            {
                if (virtualCameras[i].enabled)
                {
                    currentCamera = virtualCameras[i];
                    framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                }
            }

            normYPanAmount = framingTransposer.m_YDamping;
            
            cam = GetComponent<Transform>();
            confiner = GetComponentInChildren<CameraConfiner>();
            startPos = transform.position;
        }

        void Start() {
            
            SceneLoader.Instance.OnSceneLoadingStarted += SceneLoader_OnSceneLoadingStarted;
            SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
            SceneLoader.Instance.OnSceneReadyToPlay += SceneLoader_OnSceneReadyToPlay;
        }

        private void SceneLoader_OnSceneLoadingStarted(object sender, EventArgs e)
        {
            confiner.RemoveConfiner();
        }
        
        private void SceneLoader_OnSceneLoadingCompleted(object sender, EventArgs e)
        {
            confiner.SetConfiner();
        }
        
        private void SceneLoader_OnSceneReadyToPlay(object sender, EventArgs e)
        {
            UpdateStartPos();
        }

        void UpdateStartPos()
        {
            startPos = transform.position;
        }
        
        void LateUpdate() 
        {
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        #region Lerp the Y Damping

        public void LerpYDamping(bool isPlayerFalling)
        {
            lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
        }

        IEnumerator LerpYAction(bool isPlayerFalling)
        {
            IsLerpingYDamping = true;
            
            //grab the starting damping amount
            float startDampAmount = framingTransposer.m_YDamping;
            float endDampAmount = 0f;
            
            //determine the end damping amount
            if (isPlayerFalling)
            {
                endDampAmount = fallPanAmount;
                LerpedFromPlayerFalling = true;
            }
            else endDampAmount = normYPanAmount;
            
            //Lerp the pan amount
            float elapsedTime = 0f;
            while (elapsedTime < fallYPanTime)
            {
                elapsedTime += Time.deltaTime;
                float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallYPanTime));
                framingTransposer.m_YDamping = lerpedPanAmount;

                yield return null;
            }

            IsLerpingYDamping = false;
        }

        #endregion
    }
}
