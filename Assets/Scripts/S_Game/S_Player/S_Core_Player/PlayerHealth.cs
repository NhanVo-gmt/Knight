using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    RecoveryController recoveryController; //todo add into player health

    protected override void Awake()
    {
        base.Awake();
        recoveryController = GetComponent<RecoveryController>();
    }

    protected override bool IsInvulnerable()
    {
        return recoveryController != null && recoveryController.IsInInvulnerabiltyTime();
    }
}
