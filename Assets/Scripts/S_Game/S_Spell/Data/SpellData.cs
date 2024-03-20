using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellParams
{
    // Init Before
    public Transform playerTransform;
    public Transform enemyTransform;
    
    // Init After
    public Animator anim;
    public Transform prefabTransform;
    
    public SpellParams(){}

}

[Serializable]
public abstract class SpellData : PooledObjectData
{
    public SpellParams spellParams;
    public Action OnFinished;

    public virtual void Initialize(SpellParams spellParams)
    {
        this.spellParams = spellParams;
    }
    
    public abstract void Start();
    public abstract void Activate();
}
