using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeListSearchProvider : ScriptableObject, ISearchWindowProvider
{
    private BehaviourTree tree;
    
    public NodeListSearchProvider(BehaviourTree tree)
    {
        this.tree = tree;
    }
    
    public void Initialize(BehaviourTree tree)
    {
        this.tree = tree;
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
        searchList.Add(new SearchTreeGroupEntry(new GUIContent("List Node"), 0));  
        foreach (Node node in tree.nodes)
        {
            searchList.Add(new SearchTreeEntry(new GUIContent(node.NodeComponent.Name))
            {
                userData = node,
                level = 1
            });  
            Debug.Log(node.NodeComponent.Name);
        }
        
        return searchList;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        return true;
    }
}
