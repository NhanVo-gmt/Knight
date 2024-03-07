using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraConfiner))]
public class CameraConfinerEditor : Editor
{
    private CameraConfiner confiner;
    
    private void Awake()
    {
        confiner = (CameraConfiner)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set Confiner"))
        {
            confiner.SetConfinerEditor();            
        }
    }
}
