using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : CoreComponent
{
    [SerializeField] bool canBlink;
    [SerializeField] bool canFlash;
    
    public Action onAnimationTrigger;
    public Action onAnimationFinishTrigger;

    Health health;

    Animator anim;
    BlinkingEffect blinkingEffect;
    FlashingEffect flashingEffect;

    protected override void Awake() 
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
        AddEffect();
    }

    void AddEffect()
    {
        if (canBlink)
        {
            if (!TryGetComponent<BlinkingEffect>(out blinkingEffect))
            {
                blinkingEffect = gameObject.AddComponent<BlinkingEffect>();
            }
        }
        if (canFlash)
        {
            if (!TryGetComponent<FlashingEffect>(out flashingEffect))
            {
                flashingEffect = gameObject.AddComponent<FlashingEffect>();
            }
        }
    }

    void Start() 
    {
        health = core.GetCoreComponent<Health>();
    }

    private void OnDisable() {
    }
    
#region Animation
    
    public void Play(int id) 
    {
        anim.Play(id);
    }

    public void Play(string clipName)
    {
        anim.Play(clipName);
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

#region Effect

    public void StartHitVFX()
    {
        StartBlinking();
        StartFlashing();
    }

    public void StartBlinking()
    {
        if (blinkingEffect == null) return;

        blinkingEffect.StartBlinking();
    }

    public void StartFlashing()
    {
        if (flashingEffect == null) return;
        
        flashingEffect.StartFlashing();
    }

#endregion
}
