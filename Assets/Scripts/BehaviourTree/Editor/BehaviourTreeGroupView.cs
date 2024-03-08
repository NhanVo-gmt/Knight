using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BehaviourTreeGroupView : Group
{
    public BehaviourTreeGroup group;

    public BehaviourTreeGroupView(BehaviourTreeGroup group)
    {
        this.group = group;
        title = string.IsNullOrEmpty(group.Title) ? "New Group" : group.Title;
        
        SetPosition(new Rect(group.Position, Vector2.zero));
    }
    
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        group.Position.x = newPos.xMin;
        group.Position.y = newPos.yMin;
    }
}
