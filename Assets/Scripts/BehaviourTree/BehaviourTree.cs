using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = " Behaviour")]
public class BehaviourTree : ScriptableObject, ISerializationCallbackReceiver
{
    public Node rootNode;
    public NodeComponent.State treeState = NodeComponent.State.RUNNING;
    
    [Header("Node")]
    public List<Node> nodes = new List<Node>();

    [Header("Group")] 
    public List<BehaviourTreeGroup> groups = new List<BehaviourTreeGroup>();
    
    [SerializeField] Vector2 offset = new Vector2(300, 0);

    public void Traverse(Node node, System.Action<Node> visiter) 
    {
        if (node)
        {
            visiter.Invoke(node);

            GetAllChild(node).ForEach((n) => Traverse(n, visiter));
        }
    }

    public BehaviourTree Clone() 
    {
        BehaviourTree behaviourTree = Instantiate(this);
        behaviourTree.rootNode = rootNode.Clone();
        
        behaviourTree.nodes = new List<Node>();
        Traverse(behaviourTree.rootNode, (n) =>
        {
            behaviourTree.nodes.Add(n);
        });
        return behaviourTree;
    }

    public NodeComponent.State Update() 
    {
        if (rootNode.NodeComponent.state == NodeComponent.State.RUNNING)
        {
            treeState = rootNode.Update();
        }

        return treeState;
    }
    
    public List<Node> GetAllChild(Node parent) 
    {
        List<Node> childList = new List<Node>();

        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode != null && decoratorNode.child != null)
        {
            childList.Add(decoratorNode.child);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode != null && rootNode.child != null)
        {
            childList.Add(rootNode.child);
        }

        CompositeNode compositeNode = parent as CompositeNode;
        if (compositeNode != null && compositeNode.children != null)
        {
            return compositeNode.children;
        }

        return childList;
    }

#if UNITY_EDITOR

    #region Node
    
    public Node CreateNode(System.Type type) 
    {
        Undo.RecordObject(this, "Behaviour Tree {Createnode}");

        Node newNode = CreateInstance(type) as Node;
        newNode.name = type.Name;
        newNode.NodeComponent.guid = GUID.Generate().ToString();

        nodes.Add(newNode);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(newNode, this);
        }

        Undo.RegisterCreatedObjectUndo(newNode, "Behaviour Tree {CreateNode}");

        AssetDatabase.SaveAssets();
        
        UpdateNodes();

        return newNode;
    }

    public List<Node> PasteNode(List<Node> oldPastedNodeList)
    {
        Undo.RegisterCompleteObjectUndo(this, "Undo pasted node");
        
        List<Node> newPastedNodeList = new List<Node>();

        // Create instance of that node
        for (int i = 0; i < oldPastedNodeList.Count; i++)
        {
            Node newNode = CreateNode(oldPastedNodeList[i].GetType());
            newNode.NodeComponent.position = oldPastedNodeList[i].NodeComponent.position + offset;
            CopyNode(newNode, oldPastedNodeList[i]);
            newPastedNodeList.Add(newNode);
        }

        // Connecting to new instance of the child
        for (int i = 0; i < newPastedNodeList.Count; i++)
        {
            List<Node> childNodeList = GetAllChild(oldPastedNodeList[i]);

            for (int j = 0; j < oldPastedNodeList.Count; j++)
            {
                if (childNodeList.Contains(oldPastedNodeList[j]))
                {
                    AddChild(newPastedNodeList[i], newPastedNodeList[j]);
                }
            }
        }
        
        UpdateNodes();

        return newPastedNodeList;
    }

    void CopyNode(Node newNode, Node copyNode)
    {
        if (newNode == null || copyNode == null) return;
        
        newNode.CopyNode(copyNode);
    }

    public void DeleteNode(Node node)
    {
        Undo.RecordObject(this, "Behaviour Tree {DeleteNode}");
        nodes.Remove(node);

        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child) 
    {
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode != null)
        {
            Undo.RecordObject(decoratorNode, "Behaviour Tree {Addchild}");
            decoratorNode.child = child;
            EditorUtility.SetDirty(decoratorNode);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode != null)
        {
            Undo.RecordObject(rootNode, "Behaviour Tree {Addchild}");
            rootNode.child = child;
            EditorUtility.SetDirty(rootNode);
        }

        CompositeNode compositeNode = parent as CompositeNode;
        if (compositeNode != null)
        {
            Undo.RecordObject(compositeNode, "Behaviour Tree {Addchild}");
            compositeNode.children.Add(child);
            EditorUtility.SetDirty(compositeNode);
        }
        
        UpdateNodes();
    }

    public void RemoveChild(Node parent, Node child) 
    {
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode != null)
        {
            Undo.RecordObject(decoratorNode, "Behaviour Tree {Removechild}");
            decoratorNode.child = null;
            EditorUtility.SetDirty(decoratorNode);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode != null)
        {
            Undo.RecordObject(decoratorNode, "Behaviour Tree {Removechild}");
            rootNode.child = null;
            EditorUtility.SetDirty(rootNode);
        }

        CompositeNode compositeNode = parent as CompositeNode;
        if (compositeNode != null)
        {
            Undo.RecordObject(decoratorNode, "Behaviour Tree {Removechild}");
            compositeNode.children.Remove(child);
            EditorUtility.SetDirty(compositeNode);
        }

        UpdateNodes();
    }

    public void UpdateNodes()
    {
        foreach (Node node in nodes)
        {
            node.NodeComponent.Tree = null;
            // node.NodeComponent.Tree = this;
        }
        
        AssetDatabase.SaveAssets();
    }
    
    #endregion

    #region Group
    
    public BehaviourTreeGroup CreateGroup()
    {
        Undo.RecordObject(this, "Behaviour Tree {Creategroup}");

        BehaviourTreeGroup group = CreateInstance<BehaviourTreeGroup>();
        group.ID = GUID.Generate().ToString();

        groups.Add(group);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(group, this);
        }

        Undo.RegisterCreatedObjectUndo(group, "Behaviour Tree {Creategroup}");

        AssetDatabase.SaveAssets();

        return group;
    }
    
    #endregion
    
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        if (path != "")
        {
            foreach(Node node in nodes)
            {
                if (AssetDatabase.GetAssetPath(node) == "") 
                {
                    AssetDatabase.AddObjectToAsset(node, path);
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
#endif
    
    public void OnBeforeSerialize()
    {
        #if UNITY_EDITOR
        OnValidate();
        #endif
    }

    public void OnAfterDeserialize()
    {
        
    }


}
