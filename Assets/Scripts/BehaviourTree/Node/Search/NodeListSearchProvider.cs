#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeListSearchProvider : ScriptableObject, ISearchWindowProvider
{
    private BehaviourTree tree;
    private ActionNode node;
    private Action<ActionNode> onSetNodeCallback;
    
    public void Initialize(BehaviourTree tree, ActionNode node, Action<ActionNode> callback)
    {
        this.tree = tree;
        this.node = node;

        onSetNodeCallback = callback;
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
        searchList.Add(new SearchTreeGroupEntry(new GUIContent("List Node"), 0));
        searchList.Add(new SearchTreeEntry(new GUIContent("None"))
        {
            userData = null,
            level = 1
        });
        
        foreach (Node node in tree.nodes)
        {
            if (string.IsNullOrEmpty(node.NodeComponent.Name)) continue;
            
            searchList.Add(new SearchTreeEntry(new GUIContent(node.NodeComponent.Name))
            {
                userData = node,
                level = 1
            }); 
        }
        
        return searchList;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        onSetNodeCallback?.Invoke((ActionNode)SearchTreeEntry.userData);

        return true;
    }
}
#endif
