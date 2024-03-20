using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spell : PooledObject
{
    [SerializeField] private SpellData spellData;
    

    protected override void Awake()
    {
        base.Awake();
    }
    
    public override void Initialize(PooledObjectData data)
    {
        spellData = (SpellData)data;
        spellData.spellParams.anim = anim;
        spellData.Start();
    }

    public void Activate()
    {
        spellData.Activate();
    }
}
