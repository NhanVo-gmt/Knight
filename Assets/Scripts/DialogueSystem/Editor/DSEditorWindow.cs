using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Window
{
    public class DSEditorWindow : EditorWindow
    {
        private string path = "Assets/Scripts/DialogueSystem/Editor/View/DSVariables.uss";

        [MenuItem("Knight/Dialogue Window")]
        public static void ShowExample()
        {
            GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddStyles();
        }

        private void AddGraphView()
        {
            DSGraphView graphView = new DSGraphView();
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            
            
            rootVisualElement.styleSheets.Add(styleSheet);
        }
    }
}
