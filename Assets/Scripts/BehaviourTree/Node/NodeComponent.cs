using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeComponent 
{
    public enum State 
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public State state = State.RUNNING;
    public bool started = false;
    public string guid;

    public Vector2 position;
    [TextArea(5, 5)] public string description;

    public NodeComponent(){}
}
