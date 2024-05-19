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
        if (canvasGroup == null) return;
        if (canvasGroup.alpha != 0)
        {
            FadeOut();
        }

        slider.value = 0f;
    }


    private void SceneLoader_OnSceneLoadingStarted(object sender, EventArgs e)
    {
        FadeIn();
    }

    private void SceneLoader_OnSceneLoadingProgressChanged(object sender, float progress)
    {
        slider.value = Mathf.Clamp01(progress/ 0.9f);
    }

    public void FadeIn()
    {
        StartCoroutine(canvasGroup.FadeIn());
    }

    public void FadeOut()
    {
        if (LoadCoroutine != null)
        {
            return;
        }

        StartCoroutine(canvasGroup.FadeOut());
    }
}
