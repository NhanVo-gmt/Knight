using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassVelocityController : MonoBehaviour
{
    [Range(0, 1f)] public float ExternalInfluenceStrength = 0.1f;
    public float EaseInTime = 0.15f;
    public float EaseOutTime = 0.15f;
    public float VelocityThreshold = 5f;

    private int _externalInfluence = Shader.PropertyToID("_ExternalInfluence");

    public void InfluenceGrass(Material mat, float xVelocity)
    {
        mat.SetFloat(_externalInfluence, xVelocity);
    }
}
