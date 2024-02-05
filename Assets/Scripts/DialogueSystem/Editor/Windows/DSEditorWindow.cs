using System;
using DS.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Window
{
    public class DSEditorWindow : EditorWindow
    {
        private readonly string editorWindowStylePath = "Assets/Scripts/DialogueSystem/Editor/View/DSVariables.uss";
        private readonly string toolbarStylePath = "Assets/Scripts/DialogueSystem/Editor/View/DSToolbarStyles.uss";

        private DSGraphView graphView;

        private readonly string defaultFileName = "DialoguesFileName";
        private TextField fileNameTextField;
        private Button saveBtn;
            
        [MenuItem("Knight/Dialogue Window")]
        public static void ShowExample()
        {
            GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();
            AddStyles();
        }
        
        #region Element Addition

        private void AddGraphView()
        {
            graphView = new DSGraphView(this);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();
            fileNameTextField = DSElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
            {
                fileNameTextField.value = callback.newValue.RemoveWhitespaces();
            });
            saveBtn = DSElementUtility.CreateButton("Save", () => Save());
            toolbar.Add(fileNameTextField);
            toolbar.Add(saveBtn);
            toolbar.AddStyleSheets(toolbarStylePath);
            
            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets(editorWindowStylePath);
        }
        
        #endregion

        #region Toolbar Actions

        private void Save()
        {
            if (string.IsNullOrEmpty(fileNameTextField.value))
            {
                EditorUtility.DisplayDialog(
                    "Invalid file name.",
                    "Please ensure the file name you've typed in is valid",
                    "Ok!"
                );
                return;
            }
            
            DSIOUtility.Initialize(graphView, fileNameTextField.value);
            DSIOUtility.Save();
        }

        #endregion

        #region Utility Methods
        public void EnableSaving()
        {
            saveBtn.SetEnabled(true);
        }

        public void DisableSaving()
        {
            saveBtn.SetEnabled(false);
        }
        
        #endregion
    }
}
