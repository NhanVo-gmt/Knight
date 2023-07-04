using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;

    CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = loadingScreen.GetComponent<CanvasGroup>();
    }

    void Start() 
    {
        SceneLoader.Intance.OnSceneLoadingProgressChanged += SceneLoader_OnSceneLoadingProgressChanged;
        SceneLoader.Intance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
    }

    private void SceneLoader_OnSceneLoadingProgressChanged(object sender, float progress)
    {
        slider.value = Mathf.Clamp01(progress/ 0.9f);
    }


    private void SceneLoader_OnSceneLoadingCompleted(object sender, EventArgs e)
    {
        
    }

    IEnumerator FadeIn()
    {
        loadingScreen.SetActive(true);
        yield return Fade(1, 1);
    }

    IEnumerator FadeOut()
    {
        yield return Fade(0, 1);
        loadingScreen.SetActive(false);
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
