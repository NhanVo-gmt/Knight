using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeSearchData 
{
    public int level;
    public Node node;
}

public class NodeListSearchProvider : ScriptableObject, ISearchWindowProvider
{
    private string[] nodes;
    public NodeListSearchProvider(string[] nodes)
    {
        this.nodes = nodes;
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
        searchList.Add(new SearchTreeGroupEntry(new GUIContent("List"), 0));
        return searchList;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        return true;
    }
}
