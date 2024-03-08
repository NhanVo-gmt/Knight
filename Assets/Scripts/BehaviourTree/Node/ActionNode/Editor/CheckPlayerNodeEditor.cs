using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(CheckPlayerNode), true)]
public class CheckPlayerNodeEditor : ActionNodeEditor
{
    private SerializedProperty checkRelativePosProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty sizeProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        checkRelativePosProperty = serializedObject.FindProperty("checkRelativePos");
        radiusProperty = serializedObject.FindProperty("radius");
        sizeProperty = serializedObject.FindProperty("size");
    }

    protected override void Awake()
    {
        base.Awake();
    }
    
    public override void OnInspectorGUI()   
    {
        base.OnInspectorGUI();
        AddSearchWindow();

        EditorGUILayout.PropertyField(checkRelativePosProperty);
        EditorGUILayout.PropertyField(radiusProperty);
        EditorGUILayout.PropertyField(sizeProperty);
        
        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnChangedNodeWindow(ActionNode node)
    {
        base.OnChangedNodeWindow(node);

        if (node == null)
        {
            this.node.linkNode = null;
        }
        else if (node is CheckPlayerNode)
        {
            this.node.linkNode = node;
        }
        else
        {
            Debug.Log($"This node don't have the same type: {this.node.GetType()} & {node.GetType()}");
        }
    }

#endif
}