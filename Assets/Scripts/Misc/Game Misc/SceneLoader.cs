using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public partial class SceneLoader : SingletonObject<SceneLoader>
{
    public EventHandler OnSceneBeforeLoading;
    public EventHandler OnSceneLoadingStarted;
    public EventHandler<float> OnSceneLoadingProgressChanged;
    public EventHandler OnSceneLoadingCompleted;
    public EventHandler OnSceneReadyToPlay;
    public EventHandler<Region> OnChangedRegion;

    private Scene currentScene;
    private Region currentRegion;

    AsyncOperation loadingOperation;

    protected override void Awake()
    {
        base.Awake();
    }

    public Scene GetCurrentScene()
    {
        return currentScene;
    }

    public Region GetRegion(Scene scene)
    {
        string[] splitStr = scene.ToString().Split("Scene");
        if (splitStr.Length > 0)
            if (Enum.TryParse(splitStr[0], out Region region))
                return region;
        
        Debug.LogError($"Can not get region from {scene}");
        return Region.None;
    }

    public void ChangeScene(Scene scene, Vector2 newPos)
    {
        StartCoroutine(ChangeSceneCoroutine(scene, newPos));
    }

    IEnumerator ChangeSceneCoroutine(Scene scene, Vector2 newPos)
    {
        OnSceneBeforeLoading?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);
        
        Player.Instance.ChangeScenePosition(newPos);
        currentScene = scene;
        
        Region newRegion = GetRegion(currentScene);
        if (currentRegion != newRegion)
        {
            currentRegion = newRegion;
            OnChangedRegion?.Invoke(this, newRegion);
        }

        loadingOperation = SceneManager.LoadSceneAsync(scene.ToString());
        OnSceneLoadingStarted?.Invoke(this, EventArgs.Empty);
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
