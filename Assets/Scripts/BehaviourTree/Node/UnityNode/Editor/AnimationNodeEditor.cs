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
    private SerializedProperty nodeProperty;
    private SerializedProperty clipNameProperty;

    private AnimationNode animationNode;
    
    private void Awake()
    {
        animationNode = (AnimationNode)target;
    }

    private void OnEnable()
    {
        nodeProperty = serializedObject.FindProperty("NodeComponent");
        clipNameProperty = serializedObject.FindProperty("clipName");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((AnimationNode)target), typeof(AnimationNode), false);
        EditorGUILayout.PropertyField(nodeProperty);

        EditorGUI.BeginChangeCheck();

        GameObject selectedGameObject = Selection.activeGameObject;
        if (selectedGameObject != null)
        {
            Animator selectedAnim = selectedGameObject.GetComponentInChildren<Animator>();
            if (selectedAnim != null)
            {
                EditorGUILayout.Space(10f);
                
                string[] clipNames = selectedAnim.runtimeAnimatorController.animationClips.Select(item => ChangeClipName(item.name)).ToArray();
                if (clipNames.Length > 0)
                {
                    animationNode.clipNameIndex = EditorGUILayout.Popup("Clip: ", animationNode.clipNameIndex, clipNames);
                    animationNode.clipName = clipNames[animationNode.clipNameIndex];
                }
                else
                {
                    EditorGUILayout.LabelField($"Rule clip names:");
                    foreach(String predefinedClipName in BehaviourTreeEditorNodeSettings.AnimationClipNames) {
                        EditorGUILayout.LabelField($"{predefinedClipName}");
                    }
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

    public string ChangeClipName(string clipName)
    {
        foreach(String predefinedClipName in BehaviourTreeEditorNodeSettings.AnimationClipNames) {
            if (clipName.Equals(predefinedClipName))
            {
                return predefinedClipName;
            }
        }
        
        EditorGUILayout.LabelField($"Please fix clip name: {clipName}");
        return String.Empty;
    }
}
