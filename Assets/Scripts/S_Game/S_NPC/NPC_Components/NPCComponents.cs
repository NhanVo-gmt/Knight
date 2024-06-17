using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCComponents : MonoBehaviour
{
    protected NPCCore core;

    public virtual void Awake()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        core = GetComponentInParent<NPCCore>();
        core.AddCoreComponent(this);
    }
    public abstract void Open();
    public abstract void Close();
}
