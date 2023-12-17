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

        public bool IsLerpingYDamping { get; private set; }
        public bool LerpedFromPlayerFalling { get; set; }

        private Coroutine lerpYPanCoroutine;
        private Coroutine panCameraCoroutine;
        
        private CinemachineVirtualCamera currentCamera;
        private CinemachineFramingTransposer framingTransposer;
        private float normYPanAmount;

        private Vector2 startingTrackedObjectOffset;
        
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

            // Set the starting pan amount
            normYPanAmount = framingTransposer.m_YDamping;

            startingTrackedObjectOffset = framingTransposer.m_TrackedObjectOffset;
            
            cam = GetComponent<Transform>();
            confiner = GetComponentInChildren<CameraConfiner>();
            startPos = transform.position;
        }

        void Start() {
            
            SceneLoader.Instance.OnSceneLoadingStarted += SceneLoader_OnSceneLoadingStarted;
            SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
            SceneLoader.Instance.OnSceneReadyToPlay += SceneLoader_OnSceneReadyToPlay;
        }

        #region Confiner

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

        #endregion

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

        #region Pan Camera

        public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection,
            bool panToStartingPos)
        {
            panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
        }

        private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection,
            bool panToStartingPos)
        {
            Vector2 endPos = Vector2.zero;
            Vector2 startingPos = Vector2.zero;

            // Handle pan from trigger
            if (!panToStartingPos)
            {
                switch (panDirection)
                {
                    case PanDirection.Up:
                        endPos = Vector2.up;
                        break;
                    case PanDirection.Down:
                        endPos = Vector2.down;
                        break;
                    case PanDirection.Left:
                        endPos = Vector2.left;
                        break;
                    case PanDirection.Right:
                        endPos = Vector2.right;
                        break;
                }

                endPos *= panDistance;
                startingPos = startingTrackedObjectOffset;
                endPos += startingPos;
            }

            // Handle the direction settings when moving back to the starting position
            else
            {
                startingPos = framingTransposer.m_TrackedObjectOffset;
                endPos = startingTrackedObjectOffset;
            }

            float elapseTime = 0f;
            while (elapseTime <= panTime)
            {
                elapseTime += Time.deltaTime;
                Vector3 panLerp = Vector3.Lerp(startingPos, endPos, elapseTime / panTime);
                framingTransposer.m_TrackedObjectOffset = panLerp;
                yield return null;
            }
        }

        #endregion
    }
}
