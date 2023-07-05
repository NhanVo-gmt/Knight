using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeSettings : ScriptableObject
{
    public StyleSheet BehaviourTreeStyleSheet;
    public TextAsset ActionNodeScriptTemplate;
    public TextAsset CompositeNodeScriptTemplate;
    public TextAsset DecoratorNodeScriptTemplate;
    public string newNodeBasePath = "Assets/Plugin/BehaviourTree/Node";

    readonly string templateFolder = "Assets/Plugin/BehaviourTree/ScriptTemplate/";
    readonly string actionNodeName = "01-BehaviourTree__New Node-NewActionNode.cs.txt";
    readonly string compositeNodeName = "02-BehaviourTree__New Node-NewCompositeAction.cs.txt";
    readonly string decoratorNodeName = "03-BehaviourTree__New Node-NewDecoratorNode.cs.txt";

    void Awake() 
    {
        ActionNodeScriptTemplate = AssetDatabase.LoadAssetAtPath<TextAsset>(templateFolder + actionNodeName);
        CompositeNodeScriptTemplate = AssetDatabase.LoadAssetAtPath<TextAsset>(templateFolder + compositeNodeName);
        DecoratorNodeScriptTemplate = AssetDatabase.LoadAssetAtPath<TextAsset>(templateFolder + decoratorNodeName);
    }

    public string GetStyleSheetPath()
    {
        return AssetDatabase.GetAssetPath(BehaviourTreeStyleSheet);
    }
}
