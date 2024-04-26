using System;
using UnityEngine;

public class PlayerParent : SingletonObject<PlayerParent>
{
    // moving platform, elevators
    private MovingPlatform platform;
    
    Vector2 GetPlatformVelocity()
    {
        if (!platform) return Vector2.zero;

        return platform.GetVelocity();
    }

    public void SetMovingPlatform(MovingPlatform platform)
    {
        this.platform = platform;
    }

    public void UnsetMovingPlatform()
    {
        platform = null;
    }

    private void FixedUpdate()
    {
        Player.Instance.SetPlatformVelocity(GetPlatformVelocity());
    }
}
