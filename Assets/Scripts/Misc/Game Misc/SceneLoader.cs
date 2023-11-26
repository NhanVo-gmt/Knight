using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoader : SingletonObject<SceneLoader>
{
    public enum Scene {
        FarmScene,
        ForestScene,
        ForestScene1
    }

    public EventHandler OnSceneLoadingStarted;
    public EventHandler<float> OnSceneLoadingProgressChanged;
    public EventHandler OnSceneLoadingCompleted;
    public EventHandler OnSceneReadyToPlay;

    AsyncOperation loadingOperation;
    Vector2 spawnPos;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ChangeScene(Scene scene, Vector2 newPos)
    {
        StartCoroutine(ChangeSceneCoroutine(scene, newPos));
    }

    IEnumerator ChangeSceneCoroutine(Scene scene, Vector2 newPos)
    {
        OnSceneLoadingStarted?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);

        loadingOperation = SceneManager.LoadSceneAsync(scene.ToString());
    }
    

    void Update() {
        if (loadingOperation == null) return;

        if (!loadingOperation.isDone)
        {
            OnSceneLoadingProgressChanged?.Invoke(this, loadingOperation.progress);
        }
        else
        {
            loadingOperation = null;
            StartCoroutine(OnSceneLoadingCompletedCoroutine());
        }
    }

    IEnumerator OnSceneLoadingCompletedCoroutine()
    {
        OnSceneLoadingCompleted?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);
        
        OnSceneReadyToPlay?.Invoke(this, EventArgs.Empty);
    }
}
