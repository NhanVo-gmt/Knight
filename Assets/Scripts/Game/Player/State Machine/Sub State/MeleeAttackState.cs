using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    public MeleeAttackState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {

    }


    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Attack();
        SpawnVFX();
    }

    private void Attack()
    {
        combat.MeleeAttack(data.meleeAttackData);

        player.inputManager.UseMeleeAttackInput();
    }

    void SpawnVFX()
    {
        vfx.SpawnVFX(data.meleeAttackData.damageVFX);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
