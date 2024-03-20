using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellParams
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public Animator anim;
    
    public SpellParams(){}
    
}

public abstract class SpellData : PooledObjectData
{
    

    protected Action OnFinished;
    public abstract void Initialize(SpellParams spellParams);
    public abstract void Activate();
}
