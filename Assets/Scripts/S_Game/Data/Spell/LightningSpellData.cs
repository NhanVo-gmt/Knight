using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "LightningSpell", fileName = "ScriptableObjects/Data/Spell/Lightning")]
public class LightningSpellData : SpellData
{
    [Header("Spell Attribute")]
    public Vector2 spawnPos;
    public RuntimeAnimatorController runtimeAnim;
    
    [Header("Attack Area")]
    public Vector2 size;
    
    private Transform playerTransform;
    private Animator anim;

    private int lightningID = Animator.StringToHash("lightning");
    
    public override void Initialize(SpellParams spellParams)
    {
        playerTransform = spellParams.playerTransform;
        anim = spellParams.anim;
        anim.runtimeAnimatorController = runtimeAnim;
        
    }

    public override void Activate()
    {
        anim.Play(lightningID);
    }
}
