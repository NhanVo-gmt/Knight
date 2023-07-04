using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T>
{
    public static T Intance;

    protected virtual void Awake() 
    {
        if (Intance == null)
        {
            Intance = this as T;
            DontDestroyOnLoad(Intance);
        }
        else
        {
            Destroy(this);
        }
    }
}
