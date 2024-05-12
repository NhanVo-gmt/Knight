using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : CompositeNode
{
    public List<int> percentList = new List<int>();
    private bool decreasePercentWhenImplemented = false;
    private int decreaseAmount = 0;

    private int randNum = 0;
    private int index = 0;
    private int sum = 0;

    public override void CopyNode(Node copyNode)
    {
        base.CopyNode(copyNode);
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        
        // Ensure more percent is not valid
        for (int i = 0; i < children.Count; i++)
        {
            sum += percentList[i];
        }
    }

    protected override void OnStart()
    {
        randNum = Random.Range(0, sum + 1);
        for (int i = 0; i < percentList.Count; i++)
        {
            if (randNum <= percentList[i])
            {
                index = i;
                break;
            }

            randNum -= percentList[i];
        }
    }

    protected override void OnStop()
    {

    }

    protected override NodeComponent.State OnUpdate()
    {
        return children[index].Update();
    }
    

}
