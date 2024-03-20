using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "LightningSpell", fileName = "ScriptableObjects/Data/Spell/Lightning")]
public class LightningSpellData : SpellData
{
    [Header("Spell Attribute")]
    public RuntimeAnimatorController runtimeAnim;
    
    [Header("Attack Area")]
    public Vector2 size;

    public Vector2 offset;
    
    private Transform playerTransform;
    private Animator anim;

    private int lightningID = Animator.StringToHash("lightning");
    
    public override void Initialize(SpellParams spellParams)
    {
        base.Initialize(spellParams);
            
        playerTransform = spellParams.playerTransform;
    }

    public override void Start()
    {
        anim = spellParams.anim;
        anim.runtimeAnimatorController = runtimeAnim;
        anim.Play(lightningID);
    }


    public override void Activate()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(offset + (Vector2)spellParams.prefabTransform.position, size, 0);
        
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(1, IDamageable.DamagerTarget.Enemy, Vector2.zero); 
            }
        }
    }
}
