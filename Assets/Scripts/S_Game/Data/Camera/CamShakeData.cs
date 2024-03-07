using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CamShakeDat", menuName = "ScriptableObjects/Data/Camera/CamShakeData")]
public class CamShakeData : ScriptableObject
{
    [Header("Cam Shake")] 
    public float shakeDuration = 0.1f;
    public float shakeAmount = 1f;
    public float shakeFrequency = 2f;
}
