using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spell : PooledObject
{
    [SerializeField] private SpellData spellData;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    public override void Initialize(PooledObjectData data)
    {
        spellData = (SpellData)data.Clone();

    }

    public void Activate()
    {
        spellData.Activate();
    }
}
