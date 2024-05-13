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
        // Main cam brain
        private CinemachineBrain brain;
        
        // Child cam
        [Serializable]
        public class CameraClass
        {
            public enum CameraType
            {
                CenterPlayer,
                NoYFollow,
                LockedPositionRoom,
                LookupPlayer,
                LookdownPlayer,
            }

            public CameraType camType;
            public CinemachineVirtualCamera cam;
        }
        
        [SerializeField] private CameraClass[] virtualCameras;
        [SerializeField] private CameraClass.CameraType currentCamType;

        
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        private CinemachineFramingTransposer framingTransposer;
        private CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;
        private float normYPanAmount;
        private Vector2 startingTrackedObjectOffset;
        
        private Coroutine lerpYPanCoroutine;
        private Coroutine panCameraCoroutine;

        private CameraShake cameraShake;

        
        // Scene Cam
        public Vector2 startPos { get; private set; }

        private Transform cam;
        private CameraConfiner confiner;
        private bool canLook = true;

        protected override void Awake() 
        {
            base.Awake();
            brain = GetComponent<CinemachineBrain>();
            cameraShake = GetComponent<CameraShake>();
            
            for (int i = 0; i < virtualCameras.Length; i++)
            {
                if (virtualCameras[i].camType == CameraClass.CameraType.CenterPlayer)
                {
                    currentCamera = virtualCameras[i].cam;
                    currentCamera.enabled = true;
                    currentCamType = CameraClass.CameraType.CenterPlayer;
                    framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                    basicMultiChannelPerlin =
                        currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cameraShake.Initialize(basicMultiChannelPerlin);
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
            PopulatePlayerToCamera();
            SceneLoader.Instance.OnFirstStartGame += SceneLoader_OnFirstStartGame;
            SceneLoader.Instance.OnSceneLoadingStarted += SceneLoader_OnSceneLoadingStarted;
            SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
            SceneLoader.Instance.OnSceneReadyToPlay += SceneLoader_OnSceneReadyToPlay;
        }

        private void PopulatePlayerToCamera()
        {
            for (int i = 0; i < virtualCameras.Length; i++)
            {
                virtualCameras[i].cam.Follow = Player.Instance.transform;
                virtualCameras[i].cam.LookAt = Player.Instance.transform;
            }
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

        #region Cinamechine Brain

        public void OnPlatform()
        {
            brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        }

        public void DefaultUpdate()
        {
            brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        }

        #endregion

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

        #region Look up / down camera

        [Header("Look up/down")]
        [SerializeField] private float lerpLookDownDuration = .2f;


        public void LookNormal()
        {
            if (!canLook) return;
            SwapCamera(currentCamType, CameraClass.CameraType.CenterPlayer);
        }
        
        public void LookUp()
        {
            if (!canLook) return;
            SwapCamera(currentCamType, CameraClass.CameraType.LookupPlayer);
        }

        public void LookDown()
        {
            if (!canLook) return;
            SwapCamera(currentCamType, CameraClass.CameraType.LookdownPlayer);
        }

        public bool CanLookCamera(CameraClass.CameraType camType)
        {
            return !(camType == CameraClass.CameraType.NoYFollow ||
                     camType == CameraClass.CameraType.LockedPositionRoom);
        }

        public void SetLookCamera(CameraClass.CameraType camType)
        {
            if (CanLookCamera(camType))
            {
                canLook = true;
            }
            else canLook = false;
        }
        
        IEnumerator LerpYTrackObjectCoroutine(Vector2 offset)
        {
            Vector2 currentTrackOffset = framingTransposer.m_TrackedObjectOffset;
            float startTime = Time.time;
            while (startTime + lerpLookDownDuration >= Time.time)
            {
                framingTransposer.m_TrackedObjectOffset = Vector2.Lerp(currentTrackOffset, offset, (Time.time - startTime) / lerpLookDownDuration);
                yield return null;
            }

            framingTransposer.m_TrackedObjectOffset = offset;
        }

        #endregion

        #region Lerp the Y Damping
        
        [Header("Control for lerping for player fall/jump")] 
        [SerializeField] private float fallPanAmount = 0.25f;
        [SerializeField] private float fallYPanTime = 0.35f;
        public static float fallSpeedYDampingChangeThreshold = -15f;
        
        public bool IsLerpingYDamping { get; private set; }
        public bool LerpedFromPlayerFalling { get; set; }

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

            // Handle the direction settings when moving back to the starting movePos
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

        private void SwapCamera(CinemachineVirtualCamera fromCam, CinemachineVirtualCamera toCam)
        {
            fromCam.enabled = false;
            toCam.enabled = true;
            currentCamera = toCam;
            framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            basicMultiChannelPerlin = currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (basicMultiChannelPerlin) cameraShake.Initialize(basicMultiChannelPerlin);
        }
        
        private void SwapCamera(CameraClass.CameraType fromCamType, CameraClass.CameraType toCamType)
        {
            if (fromCamType == toCamType) return;
            
            SetLookCamera(toCamType);
            CinemachineVirtualCamera currentCam = FindCamera(fromCamType); 
            CinemachineVirtualCamera toCam = FindCamera(toCamType);

            currentCamType = toCamType; 
            SwapCamera(currentCam, toCam);
        }

        public void SwapCamera(CameraClass.CameraType cameraFromLeft, CameraClass.CameraType cameraFromRight,
            Vector2 triggerExitDirection, GameObject followObject)
        {
            // If the camera on the left and exit direction was on the right
            if (currentCamType == cameraFromLeft && triggerExitDirection.x > 0f)
            {
                currentCamType = cameraFromRight;
                SwapCamera(cameraFromLeft, cameraFromRight);
                
                if (cameraFromRight == CameraClass.CameraType.LockedPositionRoom)
                {
                    SetLockedRoom(followObject);
                }
            }
            
            else if (currentCamType == cameraFromRight && triggerExitDirection.x < 0f)
            {
                currentCamType = cameraFromLeft;
                SwapCamera(cameraFromRight, cameraFromLeft);
                
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
            cameraShake.Shake(shakeDuration, shakeAmount, frequency);
        }

        public void StopShaking()
        {
            cameraShake.StopShaking();
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
