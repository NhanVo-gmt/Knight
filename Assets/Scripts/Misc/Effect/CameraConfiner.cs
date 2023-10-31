using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


namespace Knight.Camera
{
    public class CameraConfiner : MonoBehaviour
    {
        private CinemachineConfiner2D confiner;

        private void Awake()
        {
            confiner = GetComponent<CinemachineConfiner2D>();
        }

        private void Start()
        {
            SceneLoader.Instance.OnSceneLoadingCompleted += OnSceneLoadCompleted;
        }

        private void OnSceneLoadCompleted(object sender, Vector2 e)
        {
            SetConfiner();
        }
        

        void SetConfiner()
        {
            confiner.m_BoundingShape2D = GameObject.FindWithTag("CameraConfiner").GetComponent<Collider2D>();
        }
    }
}
