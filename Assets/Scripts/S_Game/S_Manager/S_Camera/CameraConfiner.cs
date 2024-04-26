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
            SetConfiner();
        }

        public void SetConfiner()
        {
            confiner.m_BoundingShape2D = GameObject.FindWithTag("CameraConfiner")?.GetComponent<Collider2D>();
        }

        public void RemoveConfiner()
        {
            confiner.m_BoundingShape2D = null;
        }

#if UNITY_EDITOR
        public void SetConfinerEditor()
        {
            GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.FindWithTag("CameraConfiner").GetComponent<Collider2D>();
        }
#endif
    }
}
