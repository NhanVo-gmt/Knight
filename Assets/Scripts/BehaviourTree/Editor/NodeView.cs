using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> onSelectedChanged;
    public Node node;
    public Port input;
    public Port output;

    DescriptionView descriptionView;

    public NodeView(Node node) : base(BehaviourTreeUIBuilderPath.NodeViewUxmlPath) 
    {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.NodeComponent.guid;

        style.left = node.NodeComponent.position.x;
        style.top = node.NodeComponent.position.y;

        SetUpDescriptionView(node);

        SetUpClasses();
        CreateInputPorts();
        CreateOutputPorts();
    }

    void SetUpDescriptionView(Node node)
    {
        descriptionView = this.Q<DescriptionView>();
        descriptionView.text = node.NodeComponent.description;
    }

    private void SetUpClasses()
    {
        if (node is ActionNode)
        {
            AddToClassList("action");
        }
        else if (node is CompositeNode)
        {
            AddToClassList("composite");
        }
        else if (node is DecoratorNode)
        {
            AddToClassList("decorator");
        }
        else if (node is RootNode)
        {
            AddToClassList("root");
        }
    }

    private void CreateInputPorts()
    {
        if (node is ActionNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is CompositeNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is DecoratorNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is RootNode)
        {

        }

        if (input != null)
        {
            input.portName = "";
            input.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        if (node is ActionNode)
        {

        }
        else if (node is CompositeNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is DecoratorNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        else if (node is RootNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        if (output != null)
        {
            output.portName = "";
            output.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(output);
        }
    }   


    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        Undo.RecordObject(node, "Undo Set Position");
        node.NodeComponent.position.x = newPos.xMin;
        node.NodeComponent.position.y = newPos.yMin;
        EditorUtility.SetDirty(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();

        onSelectedChanged?.Invoke(this);
    }

    public void SortChildren()
    {
        CompositeNode compositeNode = node as CompositeNode;
        if (compositeNode != null)
        {
            compositeNode.children.Sort(SortByHorizontalPosition);
        }
    }

    int SortByHorizontalPosition(Node a, Node b)
    {
        return a.NodeComponent.position.x < b.NodeComponent.position.x ? -1 : 1;
    }
    
    public void UpdateState()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("failure");
        RemoveFromClassList("success");
        
        if (Application.isPlaying)
        {
            switch(node.NodeComponent.state)
            {
                case NodeComponent.State.RUNNING:
                {
                    if (node.NodeComponent.started)
                    {
                        AddToClassList("running");
                    }
                    break;
                }
                case NodeComponent.State.FAILURE:
                {
                    AddToClassList("failure");
                    break;
                }
                case NodeComponent.State.SUCCESS:
                {
                    AddToClassList("success");
                    break;
                }
            }
        }
    }

    public void UpdateDescription()
    {
        descriptionView.UpdateText(node.NodeComponent.description);
    }
}
