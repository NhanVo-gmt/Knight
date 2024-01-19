using System;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ActionNode), true)]
public class ActionNodeEditor : Editor
{
    private SerializedProperty nodeProperty;
    private SerializedProperty inheritedNodeProperty;
    
    public ActionNode node;
    
    protected virtual void OnEnable()
    {
        nodeProperty = serializedObject.FindProperty("NodeComponent");
        inheritedNodeProperty = serializedObject.FindProperty("inheritedNode");
    }

    protected virtual void Awake()
    {
        node = (ActionNode)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((ActionNode)target), typeof(ActionNode), false);
        EditorGUILayout.PropertyField(nodeProperty);
        
        GUILayout.Space(25f);
        serializedObject.ApplyModifiedProperties();
    }

    
}