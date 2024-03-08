using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(ActionNode), false)]
public class ActionNodeEditor : Editor
{
    private SerializedProperty nodeProperty;
    private SerializedProperty inheritedNodeProperty;
    
    private ActionNode node;
    
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
        
        GUILayout.Space(15f);
        
        GUILayout.Space(25f);
        serializedObject.ApplyModifiedProperties();
    }

}
#endif