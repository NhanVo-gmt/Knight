using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Window
{
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    using Data.Save;
    
    public class DSEditorWindow : EditorWindow
    {
        private readonly string editorWindowStylePath = "Assets/Scripts/DialogueSystem/Editor/View/DSVariables.uss";
        private readonly string toolbarStylePath = "Assets/Scripts/DialogueSystem/Editor/View/DSToolbarStyles.uss";

        private static DSGraphView graphView;

        private readonly string defaultFileName = "DialoguesFileName";
        private static TextField fileNameTextField;
        
        private Button        saveBtn;
        private Button        minimapBtn;
        private DropdownField dropdownField;

        private static Dictionary<string, DSGraphSaveDataSO> GraphSaveDataDictionary = new();
            
        [MenuItem("Knight/Dialogue Window", false, 100)]
        public static void OpenWindow()
        {
            GraphSaveDataDictionary = DSIOUtility.FindAllGraphs().ToDictionary(x => x.FileName, y => y);
            GetWindow<DSEditorWindow>("Dialogue Graph");
        }
        
        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            DSGraphSaveDataSO graphData = EditorUtility.InstanceIDToObject(instanceID) as DSGraphSaveDataSO;
            if (graphData != null)
            {
                OpenWindow();
                Load(graphData);
                return true;
            }

            return false;
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
            Button loadBtn = DSElementUtility.CreateButton("Load", () => Load());
            Button clearBtn = DSElementUtility.CreateButton("Clear", () => Clear());
            Button resetBtn = DSElementUtility.CreateButton("Reset", () => ResetGraph());
            minimapBtn    = DSElementUtility.CreateButton("MiniMap", () => ToggleMiniMap());

            
            dropdownField = DSElementUtility.CreateDropdownField("Assets", "Assets", GraphSaveDataDictionary.Keys.ToList(),
                callback =>
                {
                    if (GraphSaveDataDictionary[callback.newValue.RemoveWhitespaces()] != null)
                    {
                        Load(GraphSaveDataDictionary[callback.newValue.RemoveWhitespaces()]);
                    }
                });


            toolbar.Add(fileNameTextField);
            toolbar.Add(saveBtn);
            toolbar.Add(loadBtn);
            toolbar.Add(clearBtn);
            toolbar.Add(resetBtn);
            toolbar.Add(minimapBtn);
            toolbar.Add(dropdownField);
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
        
        private void Load()
        {
            string filePath = EditorUtility.OpenFilePanel("Dialogue Graphs", "Assets/DialogueSystem/Editor/Graphs", "asset");
            if (!string.IsNullOrEmpty(filePath))
            {
                Clear();
                
                DSIOUtility.Initialize(graphView, Path.GetFileNameWithoutExtension(filePath));
                DSIOUtility.Load();
            }
        }

        private static void Clear()
        {
            graphView.ClearGraph();
        }

        private void ResetGraph()
        {
            Clear();
            UpdateFileName(defaultFileName);
        }

        private void ToggleMiniMap()
        {
            graphView.ToggleMiniMap();
            minimapBtn.ToggleInClassList("ds-toolbar__button__selected");
        }

        #endregion

        #region Utility Methods

        public static void Load(DSGraphSaveDataSO graphData)
        {
            Clear();
                
            DSIOUtility.Initialize(graphView, graphData.name);
            DSIOUtility.Load();
        }

        public static void UpdateFileName(string newFileName)
        {
            fileNameTextField.value = newFileName;
        }
        
        public void EnableSaving()
        {
            saveBtn.SetEnabled(true);
        }

        public void DisableSaving()
        {
            saveBtn.SetEnabled(false);
        }

        public void PopulateGraph()
        {
            
        }
        
        #endregion
    }
}
