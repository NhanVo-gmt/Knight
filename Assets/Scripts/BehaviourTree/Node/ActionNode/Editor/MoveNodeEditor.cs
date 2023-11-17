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
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((MoveNode)target), typeof(MoveNode), false);        EditorGUILayout.PropertyField(nodeProperty);
        EditorGUILayout.PropertyField(speedProperty);
        EditorGUILayout.PropertyField(canFlyProperty);
        EditorGUILayout.PropertyField(movePosProperty);
        serializedObject.ApplyModifiedProperties();
    }


}