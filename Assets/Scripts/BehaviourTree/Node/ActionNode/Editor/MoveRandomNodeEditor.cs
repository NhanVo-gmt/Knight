using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(MoveRandomNode))]
public class MoveRandomNodeEditor : ActionNodeEditor
{
    private SerializedProperty moveRandomTypeProperty;
    private SerializedProperty heightProperty;
    private SerializedProperty widthProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty speedProperty;

    private MoveRandomNode node;

    protected override void OnEnable()
    {
        base.OnEnable();
        moveRandomTypeProperty = serializedObject.FindProperty("type");
        heightProperty = serializedObject.FindProperty("height");
        widthProperty = serializedObject.FindProperty("width");
        radiusProperty = serializedObject.FindProperty("radius");
        speedProperty = serializedObject.FindProperty("speed");
    }

    protected override void Awake()
    {
        node = (MoveRandomNode)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(moveRandomTypeProperty);
        GUILayout.Space(5f);

        if (node.type == MoveRandomNode.MoveRandomType.Box)
        {
            EditorGUILayout.PropertyField(heightProperty);
            EditorGUILayout.PropertyField(widthProperty);
        }
        else if (node.type == MoveRandomNode.MoveRandomType.Circle)
        {
            EditorGUILayout.PropertyField(radiusProperty);
        }
        GUILayout.Space(10f);
        
        EditorGUILayout.PropertyField(speedProperty);
        serializedObject.ApplyModifiedProperties();
    }
}

#endif