using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class NodeAttribute : Attribute
{
    private readonly List<String> description = new List<string>();
    
    public NodeAttribute(string fullDescription)
    {
        string currentDescription = "";

        for(int i = 0; i < fullDescription.Length; i++)
        {
            if (fullDescription[i] == '/')
            {
                description.Add(currentDescription);
                currentDescription = "";
            }
            else
            {
                currentDescription += fullDescription[i];
            }
        }
    }

    public String GetDescription(int index)
    {
        return description[index];
    }

    public int GetLength()
    {
        return description.Count;
    }
}
