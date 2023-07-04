using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonObject<SceneLoader>
{
    public enum Scene {
        FarmScene,
        ForestScene,
    }

    public EventHandler<float> OnSceneLoadingProgressChanged;
    public EventHandler OnSceneLoadingCompleted;

    AsyncOperation loadingOperation;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ChangeScene(Scene scene)
    {
        loadingOperation = SceneManager.LoadSceneAsync(scene.ToString());
        OnSceneLoadingProgressChanged?.Invoke(this, loadingOperation.progress);
    }

    void Update() {
        if (loadingOperation == null) return;

        if (!loadingOperation.isDone)
        {
            OnSceneLoadingProgressChanged?.Invoke(this, loadingOperation.progress);
        }
        else 
        {
            OnSceneLoadingCompleted?.Invoke(this, EventArgs.Empty);
            loadingOperation = null;
        }
    }
}
