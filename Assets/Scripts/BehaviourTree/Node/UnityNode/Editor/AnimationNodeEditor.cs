using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Knight.Editor;
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
            Animator selectedAnim = selectedGameObject.GetComponentInChildren<Animator>();
            if (selectedAnim != null)
            {
                
                string[] clipNames = selectedAnim.runtimeAnimatorController.animationClips.Select(item => ChangeClipName(item.name)).ToArray();
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

    public string ChangeClipName(string name)
    {
        foreach(String clipName in GameEditorSettings.AnimationClipName) {
            if (name.Contains(clipName))
            {
                return clipName;
            }
        }

        Debug.LogError("Please fix the name of the animation clip accoring to game editor settings");
        return String.Empty;
    }
}
