#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TeleportNode), false)]
public class TeleportNodeEditor : ActionNodeEditor
{
    protected SerializedProperty typeProperty;
    protected SerializedProperty positionProperty;
    
    protected ActionNode teleportNode;

    protected override void OnEnable()
    {
        base.OnEnable();
        typeProperty = serializedObject.FindProperty("type");
        positionProperty = serializedObject.FindProperty("position");
    }

    protected override void Awake()
    {
        base.Awake();
        teleportNode = (TeleportNode)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.PropertyField(typeProperty);
        EditorGUILayout.PropertyField(positionProperty);

        
        serializedObject.ApplyModifiedProperties();
    }
}

#endif
