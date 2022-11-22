using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileData data;

    public void Initialize(ProjectileData data)
    {
        this.data = data;
    }
}
