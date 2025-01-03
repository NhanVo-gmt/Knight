using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IDamageable
{
    public Action OnTrigger;
    public bool hasTrigger;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Trigger()
    {
        hasTrigger = true;
        OnTrigger?.Invoke();
    }

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return IDamageable.DamagerTarget.Trap;
    }

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return IDamageable.KnockbackType.none;
    }

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection)
    {
        if (hasTrigger) return;

        if (damagerType != IDamageable.DamagerTarget.Player) return;
     
        Trigger();
        anim.Play("Activate");
    }
}
