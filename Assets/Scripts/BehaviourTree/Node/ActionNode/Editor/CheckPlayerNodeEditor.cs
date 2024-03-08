using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(CheckPlayerNode), false)]
public class CheckPlayerNodeEditor : ActionNodeEditor
{
    private SerializedProperty checkRelativePosProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty sizeProperty;
    private CheckPlayerNode node;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        checkRelativePosProperty = serializedObject.FindProperty("checkRelativePos");
        radiusProperty = serializedObject.FindProperty("radius");
        sizeProperty = serializedObject.FindProperty("size");
    }

    protected override void Awake()
    {
        node = (CheckPlayerNode)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(checkRelativePosProperty);
        EditorGUILayout.PropertyField(radiusProperty);
        EditorGUILayout.PropertyField(sizeProperty);
        
        serializedObject.ApplyModifiedProperties();
    }


#endif
}