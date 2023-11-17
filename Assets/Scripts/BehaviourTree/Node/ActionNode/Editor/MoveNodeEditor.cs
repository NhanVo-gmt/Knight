using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MoveNode))]
public class MoveNodeEditor : Editor
{
    private SerializedProperty nodeProperty;
    private SerializedProperty speedProperty;
    private SerializedProperty canFlyProperty;
    private SerializedProperty movePosProperty;

    private void OnEnable()
    {
        nodeProperty = serializedObject.FindProperty("NodeComponent");
        speedProperty = serializedObject.FindProperty("speed");
        canFlyProperty = serializedObject.FindProperty("canFly");
        movePosProperty = serializedObject.FindProperty("movePos");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(nodeProperty);
        EditorGUILayout.PropertyField(speedProperty);
        EditorGUILayout.PropertyField(canFlyProperty);
        EditorGUILayout.PropertyField(movePosProperty);
        serializedObject.ApplyModifiedProperties();
    }


}