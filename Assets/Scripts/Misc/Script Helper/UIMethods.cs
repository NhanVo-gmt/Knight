using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIMethods
{
    public static IEnumerator FadeIn(this CanvasGroup canvasGroup)
    {
        yield return Fade(canvasGroup, 1f);
    }

    public static IEnumerator FadeOut(this CanvasGroup canvasGroup)
    {
        yield return Fade(canvasGroup, 0f);
    }
    
    public static IEnumerator Fade(this CanvasGroup canvasGroup, float targetAlpha, float duration = 0.5f)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        if (Mathf.Approximately(canvasGroup.alpha, 1f))
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
