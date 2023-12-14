using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Camera
{
    public class CameraController : SingletonObject<CameraController>
    {
        public Vector2 startPos { get; private set; }

        private Transform cam;
        private CameraConfiner confiner;

        protected override void Awake() 
        {
            base.Awake();
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
        
    }
}
