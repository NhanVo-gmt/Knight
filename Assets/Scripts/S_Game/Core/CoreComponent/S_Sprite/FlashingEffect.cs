using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorController))]
public class FlashingEffect : MonoBehaviour
{
    Coroutine flashingCoroutine;

    private SpriteRenderer sprite;
    private Material defaultMaterial;
    
    private Material flashMaterial;
    private float cooldown;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultMaterial = sprite.material;
    }
    
    private void Start()
    {
        flashMaterial = GameSettings.Instance.flashMat;
        cooldown = GameSettings.Instance.flashCoolDown;
    }

    public void StartFlashing()
    {
        StopFlashing();
        flashingCoroutine = StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        sprite.material = flashMaterial;
        yield return new WaitForSeconds(cooldown);
        sprite.material = defaultMaterial;
    }

    void StopFlashing()
    {
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            sprite.material = defaultMaterial;
        }
    }
}
