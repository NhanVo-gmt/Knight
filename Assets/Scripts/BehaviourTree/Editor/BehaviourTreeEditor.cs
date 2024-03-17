using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;
using System;
using System.Collections.Generic;

public class BehaviourTreeEditor : EditorWindow
{
    BehaviourTreeGraphView treeGraphView;
    InspectorView inspectorView;
    [NonSerialized] NodeView selectedNodeView;

    private ToolbarMenu toolbarMenu;
    
    [MenuItem("Knight/BehaviourTreeEditor")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    [OnOpenAssetAttribute(0)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        BehaviourTree tree = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTree;
        if (tree != null)
        {
            OpenWindow();
            return true;
        }

        return false;
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;


        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(BehaviourTreeUIBuilderPath.EditorUxmlPath); 
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(BehaviourTreeUIBuilderPath.EditorStyleSheetPath);

        root.styleSheets.Add(styleSheet);

        treeGraphView = root.Q<BehaviourTreeGraphView>();
        inspectorView = root.Q<InspectorView>();
        
        toolbarMenu = root.Q<ToolbarMenu>();
        AddActionToToolBar();
        
        treeGraphView.onNodeViewSelectionChanged = OnNodeSelectionChange;
        
        OnSelectionChange();
    }

    public void AddActionToToolBar()
    {
        if (toolbarMenu == null) return;

        var behaviourTrees = LoadAssets<BehaviourTree>();
        behaviourTrees.ForEach(tree =>
        {
            toolbarMenu.menu.AppendAction($"{tree.name}", (a) =>
            {
                Selection.activeObject = tree;
            });
        });
    }

    List<T> LoadAssets<T>() where T : UnityEngine.Object
    {
        string[] assetsIds = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        List<T> assets = new List<T>();
        foreach (var assetId in assetsIds)
        {
            string path = AssetDatabase.GUIDToAssetPath(assetId);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            assets.Add(asset);
        }

        return assets;
    } 

    private void OnEnable() {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
        EditorApplication.playModeStateChanged += OnPlayModeStateChange;
    }

    private void OnDisable() {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
    }

    private void OnPlayModeStateChange(PlayModeStateChange obj)
    {
        switch(obj)
        {
            case PlayModeStateChange.EnteredEditMode:
            {
                OnSelectionChange();
                break;
            }
            case PlayModeStateChange.EnteredPlayMode:
            {
                OnSelectionChange();
                break;
            }
        }
    }


    private void OnSelectionChange() {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (!tree)
        {
            if (Selection.activeGameObject)
            {
                BehaviourTreeRunner behaviourTreeRunner = Selection.activeGameObject.GetComponent<BehaviourTreeRunner>();
                if (behaviourTreeRunner)
                {
                    tree = behaviourTreeRunner.tree;
                }
            }
        }

        if (treeGraphView != null)
        {
            if (Application.isPlaying)
            {
                if (tree != null)
                {
                    treeGraphView.PopulateView(tree, this);
                }
            }
            else
            {
                if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
                {
                    treeGraphView.PopulateView(tree, this);
                }
            }
        }
        
    }

    void OnNodeSelectionChange(NodeView nodeView)
    {
        inspectorView.UpdateSelection(nodeView);
        selectedNodeView = nodeView;
    }

    private void OnInspectorUpdate() {
        treeGraphView?.UpdateNodeState();
    }

    private void Update() {
        if (!Application.isPlaying)
        {
            if (inspectorView != null)
            {
                inspectorView.DrawGizmos();
            }

            if (selectedNodeView != null)
            {
                selectedNodeView.UpdateDescription();
            }
        }
    }
    
}