#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform)), CanEditMultipleObjects]
public class MovingPlatformEditor : Editor
{
    private void OnSceneGUI()
    {
        MovingPlatform platform = (MovingPlatform)target;
        for (int i = 0; i < platform._checkpoints.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector2 newTargetPosition = Handles.PositionHandle(platform._checkpoints[i] + (Vector2)platform.transform.position, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(platform, "Change Target Position");
                platform._checkpoints[i] = newTargetPosition - (Vector2)platform.transform.position;
            }
        }

    }
}

#endif
