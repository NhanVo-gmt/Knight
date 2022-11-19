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

    protected override void Awake() 
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
#region Animation
    
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

#endregion

#region Sprite

    public void StartBlinking(float cooldown, float blinkTime)
    {
        StopBlinking();
        blinkingCoroutine = StartCoroutine(Blinking(cooldown, blinkTime));
    }

    IEnumerator Blinking(float cooldown, float blinkTime)
    {
        float startTime = Time.time;

        while (startTime + cooldown > Time.time)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(blinkTime);
        }

        yield return null;

        sprite.enabled = true;
    }

    void StopBlinking()
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
            sprite.enabled = true;
        }
    }

#endregion
}
