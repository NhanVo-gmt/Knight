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

    Coroutine LoadCoroutine;

    void Awake() {
        canvasGroup = loadingScreen.GetComponent<CanvasGroup>();
    }

    void Start() 
    {
        SceneLoader.Instance.OnSceneBeforeLoading += SceneLoader_OnSceneLoadingStarted;
        SceneLoader.Instance.OnSceneLoadingProgressChanged += SceneLoader_OnSceneLoadingProgressChanged;
        SceneLoader.Instance.OnSceneReadyToPlay += SceneLoader_OnSceneReadyToPlay;
    }

    private void SceneLoader_OnSceneReadyToPlay(object sender, EventArgs e)
    {
        if (canvasGroup.alpha != 0)
        {
            StartCoroutine(FadeOut());
        }

        slider.value = 0f;
    }


    private void SceneLoader_OnSceneLoadingStarted(object sender, EventArgs e)
    {
        LoadCoroutine = StartCoroutine(FadeIn());
    }

    private void SceneLoader_OnSceneLoadingProgressChanged(object sender, float progress)
    {
        slider.value = Mathf.Clamp01(progress/ 0.9f);
    }


    public IEnumerator FadeIn()
    {
        yield return Fade(1, 1);
    }

    public IEnumerator FadeOut()
    {
        if (LoadCoroutine != null)
        {
            yield return LoadCoroutine;
        }
        
        yield return Fade(0, 1);
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
