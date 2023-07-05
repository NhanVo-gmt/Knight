using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    BehaviourTreeGraphView graphView;
    EditorWindow window;

    public void Initialize (EditorWindow window, BehaviourTreeGraphView graphView)
    {
        this.window = window;
        this.graphView = graphView;
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Node"), level: 0)
        };

        AddSearchEntryForEachType(tree);
    
        return tree;
    }

    void AddSearchEntryForEachType(List<SearchTreeEntry> tree)
    {
        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Action Node"), level: 1));
        {

            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            List<System.Type> actionTypes = new List<System.Type>();
            Dictionary<System.Type, NodeAttribute> customTypes = new Dictionary<Type, NodeAttribute>();
        
            foreach (System.Type type in types)
            {
                var nodeAttribute = type.GetCustomAttributes(typeof(NodeAttribute), false);
                if (nodeAttribute.Length > 0)
                {
                    customTypes.Add(type, (NodeAttribute)nodeAttribute[0]);
                }
                else
                {
                    actionTypes.Add(type);
                }
            }

            AddSearchEntryForCustomTypeNode(tree, customTypes, 0); 

            actionTypes.ForEach((type) => tree.Add(CreateNodeFromType(type, 2)));
                
        }

        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Composite Node"), level: 1));
        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (System.Type type in types)
            {
                tree.Add(CreateNodeFromType(type, 2));
            }
        }

        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Decorator Node"), level: 1));
        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (System.Type type in types)
            {
                tree.Add(CreateNodeFromType(type, 2));
            }
        }
    }

    private void AddSearchEntryForCustomTypeNode(List<SearchTreeEntry> tree, Dictionary<System.Type, NodeAttribute> types, int descriptionLevel)
    {
        Dictionary<string, List<System.Type>> typeDictionary = new Dictionary<string, List<Type>>();

        foreach (System.Type type in types.Keys)
        {
            if (descriptionLevel >= types[type].GetLength())
            {
                tree.Add(CreateNodeFromType(type, 2 + descriptionLevel));
                continue;
            }
            
            string typeDescription = types[type].GetDescription(descriptionLevel);
            if (!typeDictionary.ContainsKey(typeDescription))
            {
                typeDictionary.Add(typeDescription, new List<Type>());
            }

            typeDictionary[typeDescription].Add(type);
        }

        foreach(String description in typeDictionary.Keys)
        {
            tree.Add(new SearchTreeGroupEntry(new GUIContent(description), level: 2 + descriptionLevel));
            {
                Dictionary<System.Type, NodeAttribute> childType = new Dictionary<Type, NodeAttribute>();
                foreach(Type type in typeDictionary[description])
                {
                    childType.Add(type, types[type]);
                }

                AddSearchEntryForCustomTypeNode(tree, childType, descriptionLevel + 1);
            }
        }
    }

    SearchTreeEntry CreateNodeFromType(System.Type type, int typeLevel)
    {
        return new SearchTreeEntry(new GUIContent(type.Name))
        {
            userData = type,
            level = typeLevel
        };
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var worldMousePosition = graphView.ChangeCoordinatesTo(graphView.parent, context.screenMousePosition - window.position.position);
        var localMousePosition = graphView.contentContainer.WorldToLocal(worldMousePosition);
        

        if (SearchTreeEntry.userData is System.Type)
        {
            graphView.CreateNode(SearchTreeEntry.userData as System.Type, localMousePosition);
            return true;
        }

        return false;
    }
}
