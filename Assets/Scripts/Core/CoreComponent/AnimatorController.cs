using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : CoreComponent
{

    public Action onAnimationTrigger;
    public Action onAnimationFinishTrigger;

    Coroutine blinkingCoroutine;

    Animator anim;
    SpriteRenderer sprite;
    Player player;

    protected override void Awake() 
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
    }
    
    public void Play(int id) 
    {
        anim.Play(id);
    }

    public void AnimationTrigger()
    {
        onAnimationTrigger?.Invoke();
    }

    public void AnimationFinishTrigger()
    {
        onAnimationFinishTrigger?.Invoke();
    }

    public void StartBlinking(float cooldown)
    {
        blinkingCoroutine = StartCoroutine(Blinking(cooldown));
    }

    IEnumerator Blinking(float cooldown)
    {
        while (true)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(cooldown);
        }
    }

    public void StopBlinking()
    {
        StopCoroutine(blinkingCoroutine);
        sprite.enabled = true;
    }
    
}
