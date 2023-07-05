using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AnimationNode))]
public class AnimationNodeEditor : Editor
{
    SerializedProperty clipName;
    int index = 0;

    private void OnEnable() {
        clipName = serializedObject.FindProperty("clipName");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        AnimationNode animationNode = (AnimationNode)target;
        GameObject selectedGameObject = Selection.activeGameObject;
        if (selectedGameObject != null)
        {
            Animator selectedAnim = selectedGameObject.GetComponent<Animator>();
            if (selectedAnim != null)
            {
                string[] clipNames = selectedAnim.runtimeAnimatorController.animationClips.Select(item => item.name).ToArray();
                index = EditorGUILayout.Popup("Available Clip: ", index, clipNames);

                if (EditorGUI.EndChangeCheck())
                {
                    clipName.stringValue = clipNames[index];
                }
            }
            else
            {
                EditorGUILayout.LabelField("Select an GameObject with Animator to see available clips");
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select an GameObject with Animator to see available clips");
        }

        
        serializedObject.ApplyModifiedProperties();
    }
}
