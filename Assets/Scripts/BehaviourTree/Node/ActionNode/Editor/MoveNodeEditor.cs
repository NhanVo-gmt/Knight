using System;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MoveNode))]
public class MoveNodeEditor : Editor
{
    private SerializedProperty nodeProperty;
    private SerializedProperty speedProperty;
    private SerializedProperty canFlyProperty;
    
    private SerializedProperty movePositionProperty;
    private SerializedProperty moveTypeProperty;
    
    private SerializedProperty movePosProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty moveTimeProperty;

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
        movePositionProperty = serializedObject.FindProperty("movePosition");
        moveTypeProperty = serializedObject.FindProperty("moveType");
        movePosProperty = serializedObject.FindProperty("movePos");
        radiusProperty = serializedObject.FindProperty("radius");
        moveTimeProperty = serializedObject.FindProperty("moveTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((MoveNode)target), typeof(MoveNode), false);        EditorGUILayout.PropertyField(nodeProperty);
        EditorGUILayout.PropertyField(speedProperty);
        EditorGUILayout.PropertyField(canFlyProperty);
        
        EditorGUILayout.PropertyField(movePositionProperty);
        if (moveNode.movePosition == MoveNode.MovePosition.Point)
        {
            EditorGUILayout.PropertyField(movePosProperty);
        }
        else if (moveNode.movePosition == MoveNode.MovePosition.RandomInCircle)
        {
            EditorGUILayout.PropertyField(radiusProperty);
        }
        else
        {
            EditorGUILayout.PropertyField(moveTypeProperty);
            if (moveNode.moveType == MoveNode.MoveType.Line)
            {
                EditorGUILayout.PropertyField(moveTimeProperty);
            }
        }
        
        serializedObject.ApplyModifiedProperties();
    }


}