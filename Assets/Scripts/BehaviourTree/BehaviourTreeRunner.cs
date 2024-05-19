using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    public BehaviourTreeComponent treeComponent {get; private set;}
    

    private void Awake() {
        InitializeTreeComponent();

        CloneTree();
        
    }

    void Start() 
    {
        InitializeNodeComponent();  
    }

    public void InitializeTreeComponent() 
    {
        Debug.Log(GameObject.FindWithTag("Player"));
        treeComponent = BehaviourTreeComponent.CreateTreeComponentFromGameObject(gameObject, GameObject.FindWithTag("Player"));
    }

    void CloneTree() 
    {
        tree = tree.Clone();
    }

    void InitializeNodeComponent()
    {
        tree.Traverse(tree.rootNode, (n) =>
        {
            n.OnInitialize(treeComponent);
        });
    }


    void Update()
    {
        tree.Update();
    }
}
