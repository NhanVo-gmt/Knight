using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "LightningSpell", fileName = "ScriptableObjects/Data/Spell/Lightning")]
public class LightningSpellData : SpellData
{
    [SerializeField] private float height;
    [SerializeField] private GameObject go;
    private Transform playerTransform;
    
    public override void Initialize(SpellParams spellParams)
    {
        playerTransform = spellParams.playerTransform;
    }

    public override void Activate()
    {
        
    }
}
