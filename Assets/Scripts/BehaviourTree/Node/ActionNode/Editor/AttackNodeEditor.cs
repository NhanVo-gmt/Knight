using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(AttackNode))]
public class AttackNodeEditor : Editor
{
    private SerializedProperty nodeProperty;
    private SerializedProperty damageProperty;
    private SerializedProperty shapeProperty;
    private SerializedProperty attackPosProperty;
    private SerializedProperty boxSizeProperty;
    private SerializedProperty circleRadiusProperty;

    private AttackNode node;

    private void OnEnable()
    {
        nodeProperty = serializedObject.FindProperty("NodeComponent");
        damageProperty = serializedObject.FindProperty("damage");
        shapeProperty = serializedObject.FindProperty("shape");
        attackPosProperty = serializedObject.FindProperty("attackPos");
        boxSizeProperty = serializedObject.FindProperty("boxSize");
        circleRadiusProperty = serializedObject.FindProperty("circleRadius");
    }

    private void Awake()
    {
        node = (AttackNode)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((AttackNode)target), typeof(AttackNode), false);
        EditorGUILayout.PropertyField(nodeProperty);
        EditorGUILayout.PropertyField(damageProperty);
        EditorGUILayout.PropertyField(shapeProperty);
        EditorGUILayout.PropertyField(attackPosProperty);

        if (node.shape == AttackNode.AttackShape.Box)
        {
            EditorGUILayout.PropertyField(boxSizeProperty);
        }
        else if (node.shape == AttackNode.AttackShape.Circle)
        {
            EditorGUILayout.PropertyField(circleRadiusProperty);
        }
        serializedObject.ApplyModifiedProperties();
    }
    
}

#endif