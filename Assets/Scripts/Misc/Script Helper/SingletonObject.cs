using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T>
{
    private static T intance;
    public static T Instance;

    protected virtual void Awake() 
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this as T;
            DontDestroyOnLoad(Instance);
        }
    }
    
}
