using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>{}
    
    [NonSerialized] Node selectedNode;
    
    Editor editor;
    Vector2 scrollPos;

    public InspectorView()
    {
        
    }

    public void UpdateSelection(NodeView nodeView)
    {
        Clear();
        
        selectedNode = nodeView.node;

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(nodeView.node);
        IMGUIContainer iMGUIContainer = new IMGUIContainer(() => {
            if (editor.target)
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                editor.OnInspectorGUI();
                EditorGUILayout.EndScrollView();
            }
        });

        Add(iMGUIContainer);
    }

    public void DrawGizmos() 
    {
        if (selectedNode != null)
        {
            selectedNode.DrawGizmos();
        }
    }
}
