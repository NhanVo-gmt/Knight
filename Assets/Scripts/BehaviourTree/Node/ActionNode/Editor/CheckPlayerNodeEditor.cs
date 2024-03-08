using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(CheckPlayerNode), false)]
public class CheckPlayerNodeEditor : ActionNodeEditor
{
    private SerializedProperty checkRelativePosProperty;
    private SerializedProperty radiusProperty;
    private SerializedProperty sizeProperty;
    private CheckPlayerNode node;

    private NodeListSearchProvider nodeListSearchProvider;
    
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
        node = (CheckPlayerNode)target;
        
        nodeListSearchProvider = ScriptableObject.CreateInstance<NodeListSearchProvider>();
        nodeListSearchProvider.Initialize(node.NodeComponent.Tree);
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(checkRelativePosProperty);
        EditorGUILayout.PropertyField(radiusProperty);
        EditorGUILayout.PropertyField(sizeProperty);
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Link Node:");
        
        if (GUILayout.Button("HI", EditorStyles.popup))
        {
            SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), nodeListSearchProvider);
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }


#endif
}