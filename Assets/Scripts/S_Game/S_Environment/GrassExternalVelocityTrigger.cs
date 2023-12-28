using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GrassExternalVelocityTrigger : MonoBehaviour
{
    private GrassVelocityController grassVelocityController;

    private GameObject player;
    private Rigidbody2D playerRb;

    private Material mat;

    private bool easeInCoroutineRunning;
    private bool easeOutCoroutineRunning;

    private int _externalInfluence = Shader.PropertyToID("_ExternalInfluence");
    private float startingXVelocity;
    private float velocityLastFrame;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        grassVelocityController = GetComponentInParent<GrassVelocityController>();

        mat = GetComponent<SpriteRenderer>().material;
        startingXVelocity = mat.GetFloat(_externalInfluence);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(1);
        if (other.CompareTag("Player"))
        {
            Debug.Log(Mathf.Abs(playerRb.velocity.x));
            if (!easeInCoroutineRunning &&
                Mathf.Abs(playerRb.velocity.x) > Mathf.Abs(grassVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseIn(playerRb.velocity.x * grassVelocityController.ExternalInfluenceStrength));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Mathf.Abs(velocityLastFrame) > Mathf.Abs(grassVelocityController.VelocityThreshold) &&
                Mathf.Abs(playerRb.velocity.x) < Mathf.Abs(grassVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseOut());
            }
            else if (Mathf.Abs(velocityLastFrame) < Mathf.Abs(grassVelocityController.VelocityThreshold) &&
                     Mathf.Abs(playerRb.velocity.x) > Mathf.Abs(grassVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseIn(playerRb.velocity.x * grassVelocityController.ExternalInfluenceStrength));
            }
            else if (!easeInCoroutineRunning && !easeOutCoroutineRunning &&
                     Mathf.Abs(playerRb.velocity.x) > Mathf.Abs(grassVelocityController.VelocityThreshold))
            {
                grassVelocityController.InfluenceGrass(mat, playerRb.velocity.x * grassVelocityController.ExternalInfluenceStrength);
            }

            velocityLastFrame = playerRb.velocity.x;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EaseOut());
        }
    }

    private IEnumerator EaseIn(float XVelocity)
    {
        easeInCoroutineRunning = true;

        float elapsedTime = 0;
        while (elapsedTime < grassVelocityController.EaseInTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedAmount = Mathf.Lerp(startingXVelocity, XVelocity,
                elapsedTime / grassVelocityController.EaseInTime);
            grassVelocityController.InfluenceGrass(mat, lerpedAmount);
            yield return null;
        }

        easeInCoroutineRunning = false;
    }

    private IEnumerator EaseOut()
    {
        easeOutCoroutineRunning = true;
        float currentXInfluence = mat.GetFloat(_externalInfluence);

        float elapsedTime = 0f;
        while (elapsedTime < grassVelocityController.EaseOutTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedAmount = Mathf.Lerp(currentXInfluence, startingXVelocity,
                elapsedTime / grassVelocityController.EaseOutTime);
            grassVelocityController.InfluenceGrass(mat, lerpedAmount);
            yield return null;
        }

        easeOutCoroutineRunning = false;
    }
    
    
}
