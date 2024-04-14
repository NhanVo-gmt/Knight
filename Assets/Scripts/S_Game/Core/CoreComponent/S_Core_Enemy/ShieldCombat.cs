using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCombat
{
    private bool canUseShield = true;

    private Movement movement;

    public ShieldCombat(Movement movement, bool hasShield)
    {
        this.movement = movement;
        ToggleShield(false);
    }

    public bool CanShield (Vector2 attackDirection)
    {
        float angle = Vector2.Angle(-movement.faceDirection, attackDirection);
        if (angle <= 90)
        {
            return true;
        }

        return false;
    }

    public void ToggleShield(bool isActive)
    {
        canUseShield = isActive;
    }
}
