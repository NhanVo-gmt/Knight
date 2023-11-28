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
        foreach(String clipName in BehaviourTreeEditorNodeSettings.AnimationClipNames) {
            if (name.Equals(clipName))
            {
                return clipName;
            }
        }

        EditorGUILayout.LabelField("Please fix the name of the animation clip according to game editor settings");
        return String.Empty;
    }
}
