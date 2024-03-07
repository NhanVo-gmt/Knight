using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackBoardView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BlackBoardView, VisualElement.UxmlTraits>{}
    
    private IMGUIContainer container;

    public BlackBoardView()
    {
        
    }

    public void UpdateView(SerializedObject treeObject, SerializedProperty blackboardProperty)
    {
        container = hierarchy.Children().OfType<IMGUIContainer>().FirstOrDefault();
        container.onGUIHandler = () =>
        {
            if (treeObject != null && treeObject.targetObject != null)
            {
                treeObject.Update();
                EditorGUILayout.PropertyField(blackboardProperty);
                treeObject.ApplyModifiedProperties();
            }
        };
    }
}
