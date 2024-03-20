using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spell : MonoBehaviour
{
    [SerializeField] private SpellData spellData;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    private void Initialize(PooledObjectData data, SpellParams spellParams)
    {
        spellData = (SpellData)data.Clone();

        spellData.Initialize(spellParams);
    }

    public void Activate()
    {
        spellData.Activate();
    }
}
