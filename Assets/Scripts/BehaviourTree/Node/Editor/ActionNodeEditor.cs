using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(ActionNode), false)]
public class ActionNodeEditor : Editor
{
    protected SerializedProperty nodeProperty;
    protected SerializedProperty linkNodeProperty;
    
    protected NodeListSearchProvider nodeListSearchProvider;
    
    protected ActionNode node;

    private bool searchGroup;
    
    protected virtual void OnEnable()
    {
        nodeProperty = serializedObject.FindProperty("NodeComponent");
        linkNodeProperty = serializedObject.FindProperty("linkNode");
    }

    protected virtual void Awake()
    {
        node = (ActionNode)target;
        CreateSearchWindow();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((ActionNode)target), typeof(ActionNode), false);
        EditorGUILayout.PropertyField(nodeProperty);
        
        GUILayout.Space(15f);
        
        GUILayout.Space(25f);
        serializedObject.ApplyModifiedProperties();
    }

    protected void CreateSearchWindow()
    {
        nodeListSearchProvider = ScriptableObject.CreateInstance<NodeListSearchProvider>();
        nodeListSearchProvider.Initialize(node.NodeComponent.Tree, node, OnChangedNodeWindow);
    }

    protected virtual void OnChangedNodeWindow(ActionNode node)
    {
        
    }

    protected void AddSearchWindow()
    {
        searchGroup = EditorGUILayout.Foldout(searchGroup, "Link");

        if (searchGroup)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Link Node:", GUILayout.ExpandWidth(false), GUILayout.Width(150));

            string nodeLinkName = node.linkNode == null ? "None" : node.linkNode.NodeComponent.Name;
            if (GUILayout.Button($"{nodeLinkName}", EditorStyles.popup, GUILayout.MinWidth(100)))
            {
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), nodeListSearchProvider);
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.PropertyField(linkNodeProperty);
            
        }
        
        GUILayout.Space(10f);
    }

    protected bool HasLinkNode()
    {
        return node.linkNode != null;
    }
}
#endif