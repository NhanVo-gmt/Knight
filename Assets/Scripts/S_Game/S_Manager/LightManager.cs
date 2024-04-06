using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : SingletonObject<LightManager>
{
    private Light2D playerLight;
    private float currentPlayerIntensity;
    private readonly float fadePlayerLightDuration = 0.8f;
    
    private Light2D globalLight;
    private float currentIntensity;
    private readonly float fadeGlobalLightDuration = 1f;
    
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SceneLoader.Instance.OnSceneLoadingCompleted += OnSceneLoadCompleted;
        LoadGlobalLight();
    }
    
    private void OnSceneLoadCompleted(object sender, EventArgs e)
    {
        LoadGlobalLight();
        LoadPlayerLight();
    }

    private void LoadGlobalLight()
    {
        if (GameObject.FindWithTag("GlobalLight") == null)
        {
            Debug.LogError("Please add global light with the same Tag");
        }
        
        globalLight = GameObject.FindWithTag("GlobalLight").GetComponent<Light2D>();
        currentIntensity = globalLight.intensity;
    }

    private void LoadPlayerLight()
    {
        playerLight = Player.Instance.light;
        currentPlayerIntensity = playerLight.intensity;
    }
    

    public IEnumerator FadeIn()
    {
        StartCoroutine(FadeLight(playerLight, 0, fadePlayerLightDuration));
        yield return FadeLight(globalLight, 0, fadeGlobalLightDuration);
    }

    public IEnumerator FadeOut()
    {
        StartCoroutine(FadeLight(playerLight, currentPlayerIntensity, fadePlayerLightDuration));
        yield return FadeLight(globalLight, currentIntensity, fadeGlobalLightDuration);
    }

    public static IEnumerator FadeLight(Light2D light, float targetIntensity, float duration)
    {
        if (light == null) yield break;
        
        float startIntensity = light.intensity;
        
        float time = 0;
        while (time < duration)
        {
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        light.intensity = targetIntensity;  
    }
}
