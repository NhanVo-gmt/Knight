using System;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MoveNode))]
public class MoveNodeEditor : Editor
{
    private SerializedProperty nodeProperty;
    private SerializedProperty speedProperty;
    private SerializedProperty canFlyProperty;
    private SerializedProperty moveTypeProperty;
    private SerializedProperty movePosProperty;
    private SerializedProperty radiusProperty;

    private MoveNode moveNode;

    private void Awake()
    {
        moveNode = (MoveNode)target;
    }

    private void OnEnable()
    {
        nodeProperty = serializedObject.FindProperty("NodeComponent");
        speedProperty = serializedObject.FindProperty("speed");
        canFlyProperty = serializedObject.FindProperty("canFly");
        moveTypeProperty = serializedObject.FindProperty("moveType");
        movePosProperty = serializedObject.FindProperty("movePos");
        radiusProperty = serializedObject.FindProperty("radius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((MoveNode)target), typeof(MoveNode), false);        EditorGUILayout.PropertyField(nodeProperty);
        EditorGUILayout.PropertyField(speedProperty);
        EditorGUILayout.PropertyField(canFlyProperty);
        
        EditorGUILayout.PropertyField(moveTypeProperty);
        if (moveNode.moveType == MoveNode.MoveType.Point)
        {
            EditorGUILayout.PropertyField(movePosProperty);
        }
        else if (moveNode.moveType == MoveNode.MoveType.RandomInCircle)
        {
            EditorGUILayout.PropertyField(radiusProperty);
        }
        
        serializedObject.ApplyModifiedProperties();
    }


}