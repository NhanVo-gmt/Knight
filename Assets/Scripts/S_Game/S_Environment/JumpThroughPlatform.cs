using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JumpThroughPlatform : MonoBehaviour
{
    private TilemapCollider2D col;
    private float waitTimeBeforeTurnOnCollider = 0.5f;

    private void Awake()
    {
        col = GetComponent<TilemapCollider2D>();
    }

    public void JumpThrough()
    {
        col.enabled = false;
        StartCoroutine(EnableColliderCoroutine());
    }

    IEnumerator EnableColliderCoroutine()
    {
        yield return new WaitForSeconds(waitTimeBeforeTurnOnCollider);

        col.enabled = true;
    }
}
