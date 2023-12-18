using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/JumpData", fileName = "JumpData")]
public class JumpData : ScriptableObject
{
    [Header("Gravity")] 
    [Space(5)] 
    public float fallGravityMult; // Mult with gravityScale when player is falling
    public float fallMaxSpeed;
    [Space(5)] 
    public float fastFallGravityMult; // Let player fall faster if they input the downward button
    public float maxFastFallSpeed;
    [HideInInspector] public float gravityStrength; // Downward force needed for desired JumpHeight and jumpTimeToApex
    [HideInInspector] public float gravityScale;

    
    [Header("Jump")]
    public float jumpHeight;
    public float jumpTimeToApex; // Time between applying the jump force and reaching the jump height; 
    [HideInInspector] public float jumpForce; // The actual force applied;

    [Header("Both Jump")] 
    public float jumpCutGravityMult; // Multiplier to increase gravity if the player release the button
    [Range(0f, 1)] public float jumpHangGravityMult; // Reduces gravity while close to apex of the jump
    public float jumpHangTimeThreshold; // Speeds (closes to 0) where player experiences extra "jump hang"
    [Space(0.5f)] 
    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;

    [Header("Sound")]
    public AudioClip clip;

    [Header("VFX")]
    public PooledObjectData jumpVFX;

    private void OnValidate()
    {
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        gravityScale = gravityStrength / Physics2D.gravity.y;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
    }
}
