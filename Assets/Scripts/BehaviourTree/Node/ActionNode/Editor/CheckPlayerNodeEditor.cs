#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CheckPlayerNode), true)]
public class CheckPlayerNodeEditor : ActionNodeEditor
{
    private SerializedProperty checkTypeProperty;
    private SerializedProperty checkPosTypeProperty;
    private SerializedProperty checkRelativePosProperty;
    private SerializedProperty checkPosProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty sizeProperty;

    private CheckPlayerNode checkPlayerNode;

    protected override void OnEnable()
    {
        base.OnEnable();

        checkTypeProperty = serializedObject.FindProperty("checkType");
        checkPosTypeProperty = serializedObject.FindProperty("checkPosType");
        checkRelativePosProperty = serializedObject.FindProperty("checkRelativePos");
        checkPosProperty = serializedObject.FindProperty("checkPos");
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
            EditorGUILayout.PropertyField(checkPosTypeProperty);

            switch (checkPlayerNode.checkPosType)
            {
                case CheckPlayerNode.CheckPositionType.Relative:
                    EditorGUILayout.PropertyField(checkRelativePosProperty);
                    break;
                case CheckPlayerNode.CheckPositionType.World:
                    EditorGUILayout.PropertyField(checkPosProperty);
                    break;
            }

            switch (checkPlayerNode.checkType)
            {
                case CheckPlayerNode.CheckType.Box:
                    EditorGUILayout.PropertyField(sizeProperty);
                    break;
                case CheckPlayerNode.CheckType.Circle:
                    EditorGUILayout.PropertyField(radiusProperty);
                    break;
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
