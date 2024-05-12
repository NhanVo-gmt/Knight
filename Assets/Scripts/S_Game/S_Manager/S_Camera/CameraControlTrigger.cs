using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Knight.Camera;
using UnityEditor;
using UnityEngine;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (customInspectorObjects.swapCameras)
            {
                Vector2 exitDistance = (other.transform.position - col.bounds.center).normalized;
                CameraController.Instance.SwapCamera(customInspectorObjects.cameraOnLeft, customInspectorObjects.cameraOnRight, exitDistance, customInspectorObjects.roomToFollow);
            }
            
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraController.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (customInspectorObjects.swapCameras)
            {
                Vector2 exitDistance = (other.transform.position - col.bounds.center).normalized;
                CameraController.Instance.SwapCamera(customInspectorObjects.cameraOnLeft, customInspectorObjects.cameraOnRight, exitDistance, customInspectorObjects.roomToFollow);
            }
            
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraController.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
            }
        }
    }
}

[System.Serializable]
public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CameraController.CameraClass.CameraType cameraOnLeft;
    [HideInInspector] public CameraController.CameraClass.CameraType cameraOnRight;
    [HideInInspector] public GameObject roomToFollow;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
}

public enum PanDirection
{
    Up, Down, Left, Right
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraControlTrigger))]
public class CameraControlTriggerEditor : Editor
{
    private CameraControlTrigger cameraControlTrigger;

    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (cameraControlTrigger.customInspectorObjects.swapCameras)
        {
            cameraControlTrigger.customInspectorObjects.cameraOnLeft = (CameraController.CameraClass.CameraType)EditorGUILayout.EnumPopup("Camera on left", cameraControlTrigger.customInspectorObjects.cameraOnLeft);
            cameraControlTrigger.customInspectorObjects.cameraOnRight = (CameraController.CameraClass.CameraType)EditorGUILayout.EnumPopup("Camera on right", cameraControlTrigger.customInspectorObjects.cameraOnRight);

            if (cameraControlTrigger.customInspectorObjects.cameraOnLeft ==
                CameraController.CameraClass.CameraType.LockedPositionRoom ||
                cameraControlTrigger.customInspectorObjects.cameraOnRight ==
                CameraController.CameraClass.CameraType.LockedPositionRoom)
            {
                cameraControlTrigger.customInspectorObjects.roomToFollow =
                    EditorGUILayout.ObjectField("Locked Room:" ,cameraControlTrigger.customInspectorObjects.roomToFollow, typeof(GameObject), true) as GameObject;
            }
        }

        if (cameraControlTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraControlTrigger.customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup(
                "Camera Pan Direction", cameraControlTrigger.customInspectorObjects.panDirection);
            
            cameraControlTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance",
                cameraControlTrigger.customInspectorObjects.panDistance);
            cameraControlTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time",
                cameraControlTrigger.customInspectorObjects.panTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}
#endif
