using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/MoveData", fileName = "MoveData")]
public class MoveData : ScriptableObject
{
    [Header("Move")]
    public float moveMaxSpeed; //Target speed want to reach
    public float moveAcceleration; // The speed to accelerate to max speed
    [HideInInspector] public float moveAccelAmount; // The actual force(multiplied with the speedDiff) applied to the player
    public float moveDeceleration; //The speed to deccelerate
    [HideInInspector] public float moveDeccelAmount;
    
    [Space(5)] 
    
    [Range(0f, 1)] public float accelInAir;
    [Range(0f, 1)] public float deccelInAir;

    [Space(5)] public bool doConserveMomentum = true;

    [Header("Sound")] public AudioClip clip;
    
    // Called when the scriptable object is updated
    private void OnValidate()
    {
        //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
        moveAccelAmount = (50 * moveAcceleration) / moveMaxSpeed;
        moveDeccelAmount = (50 * moveDeceleration) / moveMaxSpeed;
    }
    
}
