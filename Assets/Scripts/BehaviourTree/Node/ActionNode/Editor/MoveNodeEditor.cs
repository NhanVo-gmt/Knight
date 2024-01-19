using System;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MoveNode), true)]
public class MoveNodeEditor : ActionNodeEditor
{
    private SerializedProperty speedProperty;
    private SerializedProperty canFlyProperty;
    
    private SerializedProperty movePositionProperty;
    private SerializedProperty moveTypeProperty;
    
    private SerializedProperty movePosProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty moveTimeProperty;
    private SerializedProperty moveVectorProperty;

    private MoveNode moveNode;

    protected override void Awake()
    {
        base.Awake();
        moveNode = (MoveNode)target;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        speedProperty = serializedObject.FindProperty("speed");
        canFlyProperty = serializedObject.FindProperty("canFly");
        movePositionProperty = serializedObject.FindProperty("movePosition");
        moveTypeProperty = serializedObject.FindProperty("moveType");
        movePosProperty = serializedObject.FindProperty("movePos");
        radiusProperty = serializedObject.FindProperty("radius");
        moveTimeProperty = serializedObject.FindProperty("moveTime");
        moveVectorProperty = serializedObject.FindProperty("moveVector");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        serializedObject.Update();
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
        else if (moveNode.movePosition == MoveNode.MovePosition.Vector)
        {
            EditorGUILayout.PropertyField(moveVectorProperty);
            EditorGUILayout.PropertyField(moveTimeProperty);
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