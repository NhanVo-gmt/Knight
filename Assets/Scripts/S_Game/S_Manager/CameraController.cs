using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Knight.Camera
{
    public class CameraController : SingletonObject<CameraController>
    {
        [Serializable]
        public class CameraClass
        {
            public enum CameraType
            {
                CenterPlayer,
                NoYFollow,
                LockedPositionRoom,
            }

            public CameraType camType;
            public CinemachineVirtualCamera cam;
        }
        [SerializeField] private CameraClass[] virtualCameras;

        [Header("Control for lerping for player fall/jump")] 
        [SerializeField] private float fallPanAmount = 0.25f;
        [SerializeField] private float fallYPanTime = 0.35f;
        public static float fallSpeedYDampingChangeThreshold = -15f;

        public bool IsLerpingYDamping { get; private set; }
        public bool LerpedFromPlayerFalling { get; set; }

        private Coroutine lerpYPanCoroutine;
        private Coroutine panCameraCoroutine;
        
        private CinemachineVirtualCamera currentCamera;
        private CinemachineFramingTransposer framingTransposer;
        private float normYPanAmount;
        private Vector2 startingTrackedObjectOffset;
        
        private CinemachineBasicMultiChannelPerlin camShake;
        private float shakeDuration;

        
        // Scene Cam
        public Vector2 startPos { get; private set; }

        private Transform cam;
        private CameraConfiner confiner;

        protected override void Awake() 
        {
            base.Awake();

            for (int i = 0; i < virtualCameras.Length; i++)
            {
                if (virtualCameras[i].camType == CameraClass.CameraType.CenterPlayer)
                {
                    currentCamera = virtualCameras[i].cam;
                    framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                    camShake = currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    confiner = currentCamera.GetComponent<CameraConfiner>();
                }
            }

            // Set the starting pan amount
            normYPanAmount = framingTransposer.m_YDamping;

            startingTrackedObjectOffset = framingTransposer.m_TrackedObjectOffset;
            
            cam = GetComponent<Transform>();
            confiner = GetComponentInChildren<CameraConfiner>();
            startPos = transform.position;
            
            // Getting bloom component
            volume.profile.TryGet(out bloom);
        }

        void Start()
        {

            SceneLoader.Instance.OnFirstStartGame += SceneLoader_OnFirstStartGame;
            SceneLoader.Instance.OnSceneLoadingStarted += SceneLoader_OnSceneLoadingStarted;
            SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
            SceneLoader.Instance.OnSceneReadyToPlay += SceneLoader_OnSceneReadyToPlay;
        }
        
        private void SceneLoader_OnFirstStartGame(object sender, EventArgs e)
        {
            for (int i = 0; i < virtualCameras.Length; i++)
            {
                if (virtualCameras[i].camType == CameraClass.CameraType.CenterPlayer)
                {
                    virtualCameras[i].cam.enabled = true;
                }
            }
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
        
        #region Swap Camera

        public void SwapCamera(CameraClass.CameraType cameraFromLeft, CameraClass.CameraType cameraFromRight,
            Vector2 triggerExitDirection, GameObject followObject)
        {
            CinemachineVirtualCamera camRight = FindCamera(cameraFromRight); 
            CinemachineVirtualCamera camLeft = FindCamera(cameraFromLeft); 
            // If the camera on the left and exit direction was on the right
            if (currentCamera == camLeft && triggerExitDirection.x > 0f)
            {
                camRight.enabled = true;
                camLeft.enabled = false;
                currentCamera = camRight;
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                
                if (cameraFromRight == CameraClass.CameraType.LockedPositionRoom)
                {
                    SetLockedRoom(followObject);
                }
            }
            
            else if (currentCamera == camRight && triggerExitDirection.x < 0f)
            {
                camLeft.enabled = true;
                camRight.enabled = false;
                currentCamera = camLeft;
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                
                if (cameraFromLeft == CameraClass.CameraType.LockedPositionRoom)
                {
                    SetLockedRoom(followObject);
                }
            }
        }

        private void SetLockedRoom(GameObject room)
        {
            currentCamera.Follow = room.transform;
        }
        
        #endregion

        #region Find Cam

        public CinemachineVirtualCamera FindCamera(CameraClass.CameraType type)
        {
            foreach (CameraClass camClass in virtualCameras)
            {
                if (camClass.camType == type)
                {
                    return camClass.cam;
                }
            }

            return null;
        }

        #endregion

        #region Shake Cam
        
        public void Shake(float shakeDuration, float shakeAmount, float frequency)
        {
            this.shakeDuration = shakeDuration;
            camShake.m_AmplitudeGain = shakeAmount;
            camShake.m_FrequencyGain = frequency;
        }

        private void StopShaking()
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
        

        #endregion

        #region Post Processing
        [Header("Post Processing")]
        [SerializeField] private Volume volume;

        private Bloom bloom;

        private float startBloomThreshold = 1f;
        private float normalBloomThreshold = 1.5f;
        private float bloomLerpTime = .8f;
        
        public void Bloom()
        {
            StartCoroutine(BloomCoroutine());
        }

        IEnumerator BloomCoroutine()
        {
            float startTime = 0;
            while (startTime <= bloomLerpTime)
            {
                float lerpVal = Mathf.Lerp(startBloomThreshold, normalBloomThreshold, startTime / bloomLerpTime);
                bloom.threshold.value = lerpVal;
                startTime += Time.deltaTime;
                
                yield return null;
            }

            bloom.threshold.value = normalBloomThreshold;
        }

        #endregion
        
    }
}
