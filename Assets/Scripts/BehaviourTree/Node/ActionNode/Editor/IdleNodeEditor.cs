using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(IdleNode))]
public class IdleNodeEditor : Editor
{
    private SerializedProperty nodeProperty;
    private SerializedProperty idleTimeProperty;

    private void OnEnable()
    {
        nodeProperty = serializedObject.FindProperty("NodeComponent");
        idleTimeProperty = serializedObject.FindProperty("idleTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((IdleNode)target), typeof(IdleNode), false);        
        EditorGUILayout.PropertyField(nodeProperty);
        EditorGUILayout.PropertyField(idleTimeProperty);
        serializedObject.ApplyModifiedProperties();
    }


}