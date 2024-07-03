using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteMethods
{
    public static IEnumerator FadeIn(this SpriteRenderer sprite, float duration = 0.5f)
    {
        yield return Fade(sprite, 1f, duration);
    }

    public static IEnumerator FadeOut(this SpriteRenderer sprite, float duration = 0.5f)
    {
        yield return Fade(sprite, 0f, duration);
    }
    
    public static IEnumerator Fade(this SpriteRenderer sprite, float targetAlpha, float duration = 0.5f)
    {
        Color startColor = sprite.color;
        Color endColor = sprite.color;
        endColor.a = targetAlpha;
        float time = 0f;
        while (time < duration)
        {
            sprite.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        sprite.color = endColor;    
    }
}
