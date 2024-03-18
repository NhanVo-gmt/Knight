using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckGroundNode : ActionNode
{
    [Serializable]
    public class CheckGroundNodeSingle
    {
        public LayerMask layerMask;
        public Vector2 size;
        public Vector2 relativeStartPos;
        public Color gizmosColor;
        public bool shouldBe;
        private Vector2 startPos;

        public CheckGroundNodeSingle Clone()
        {
            CheckGroundNodeSingle newNodeSingle = new CheckGroundNodeSingle();
            newNodeSingle.layerMask = layerMask;
            newNodeSingle.size = size;
            newNodeSingle.relativeStartPos = relativeStartPos;
            newNodeSingle.gizmosColor = gizmosColor;
            newNodeSingle.shouldBe = shouldBe;
            return newNodeSingle;
        }

        public void OnStart(Vector2 position)
        {
            startPos = relativeStartPos + position;
        }
        
        public bool CheckGround()
        {
            Collider2D[] cols = Physics2D.OverlapBoxAll(startPos, size, 0, layerMask);
            if (cols.Length > 0)
            {
                if (!shouldBe) return false;
                return true;
            }

            if (shouldBe) return false;
            return true;
        }
    }

    public List<CheckGroundNodeSingle> checkGroundList = new List<CheckGroundNodeSingle>();
    
    public override void CopyNode(Node copyNode)
    {
        CheckGroundNode node = copyNode as CheckGroundNode;

        if (node)
        {
            foreach (CheckGroundNodeSingle nodeSingle in node.checkGroundList)
            {
                checkGroundList.Add(nodeSingle.Clone());
            }
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        foreach (CheckGroundNodeSingle node in checkGroundList)
        {
            node.OnStart(treeComponent.transform.position);
        }
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        if (CheckAll())
        {
            return NodeComponent.State.SUCCESS;
        }

        return NodeComponent.State.FAILURE;
    }

    bool CheckAll()
    {
        foreach (CheckGroundNodeSingle node in checkGroundList)
        {
            if (!node.CheckGround()) return false;
        }

        return true;
    }

    

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        foreach (CheckGroundNodeSingle node in checkGroundList)
        {
            GizmosDrawer.color = node.gizmosColor;
            GizmosDrawer.DrawWireCube((Vector2)selectedGameObject.transform.position + node.relativeStartPos, node.size);
        }
    }
}
