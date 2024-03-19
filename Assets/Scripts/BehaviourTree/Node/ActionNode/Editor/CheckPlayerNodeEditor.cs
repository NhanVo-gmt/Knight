#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CheckPlayerNode), true)]
public class CheckPlayerNodeEditor : ActionNodeEditor
{
    private SerializedProperty checkTypeProperty;
    private SerializedProperty checkRelativePosProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty sizeProperty;

    private CheckPlayerNode checkPlayerNode;

    protected override void OnEnable()
    {
        base.OnEnable();

        checkTypeProperty = serializedObject.FindProperty("checkType");
        checkRelativePosProperty = serializedObject.FindProperty("checkRelativePos");
        radiusProperty = serializedObject.FindProperty("radius");
        sizeProperty = serializedObject.FindProperty("size");
    }

    protected override void Awake()
    {
        base.Awake();
        checkPlayerNode = (CheckPlayerNode)target;
    }
    
    public override void OnInspectorGUI()   
    {
        base.OnInspectorGUI();
        AddSearchWindow();
        
        if (!HasLinkNode())
        {
            EditorGUILayout.PropertyField(checkTypeProperty);
            EditorGUILayout.PropertyField(checkRelativePosProperty);
            
            if (checkPlayerNode.checkType == CheckPlayerNode.CheckType.Box)
            {
                EditorGUILayout.PropertyField(sizeProperty);
            }
            else if (checkPlayerNode.checkType == CheckPlayerNode.CheckType.Circle)
            {
                EditorGUILayout.PropertyField(radiusProperty);
            }
        }
        
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
            this.node.CopyNode(node);
        }
        else
        {
            Debug.Log($"This node don't have the same type: {this.node.GetType()} & {node.GetType()}");
        }
        
        EditorUtility.SetDirty(node);
    }
}
#endif
