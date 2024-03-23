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
    private GameObject currentSelectedGameObject;

    public InspectorView()
    {
        
    }

    public void UpdateSelection(NodeView nodeView)
    {
        Clear();
        
        selectedNode = nodeView.node;
        DrawGizmos(true);

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

    public void DrawGizmos(bool changedNode = false) 
    {
        if (selectedNode != null && Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<BehaviourTreeRunner>() != null)
        {
            GizmosDrawer.Begin();
            selectedNode.DrawGizmos(Selection.activeGameObject);
            GizmosDrawer.End();
            
            if (changedNode) GizmosDrawer.SetDirty();
        }
    }
}
