using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public partial class SceneLoader : SingletonObject<SceneLoader>, IDataPersistence
{
    public EventHandler OnFirstStartGame;
    
    public EventHandler OnSceneBeforeLoading;
    public EventHandler OnSceneLoadingStarted;
    public EventHandler<float> OnSceneLoadingProgressChanged;
    public EventHandler OnSceneLoadingCompleted;
    public EventHandler OnSceneReadyToPlay;
    public EventHandler OnScenePlay;
    public EventHandler<SceneLoaderEnum.Region> OnChangedRegion;

    private SceneLoaderEnum.Scene currentScene = SceneLoaderEnum.Scene.FarmScene;
    private SceneLoaderEnum.Region currentRegion = SceneLoaderEnum.Region.Farm;

    private Vector2 playerStartPos;

    AsyncOperation loadingOperation;

    protected override void Awake()
    {
        base.Awake();
        currentScene = GetSceneFromUnityScene(SceneManager.GetActiveScene().name);
        currentRegion = GetRegion(currentScene);
    }

    public SceneLoaderEnum.Scene GetCurrentScene()
    {
        return currentScene;
    }

    public SceneLoaderEnum.Region GetCurrentRegion()
    {
        return currentRegion;
    }

    public SceneLoaderEnum.Scene GetSceneFromUnityScene(string sceneName)
    {
        if (Enum.TryParse(sceneName, out SceneLoaderEnum.Scene scene))
            return scene;
        
        Debug.LogError($"Can not get scene from {sceneName}");
        return SceneLoaderEnum.Scene.MenuScene;
    }

    public SceneLoaderEnum.Region GetRegion(SceneLoaderEnum.Scene scene)
    {
        string[] splitStr = scene.ToString().Split("Scene");
        if (splitStr.Length > 0)
            if (Enum.TryParse(splitStr[0], out SceneLoaderEnum.Region region))
                return region;
        
        Debug.LogError($"Can not get region from {scene}");
        return SceneLoaderEnum.Region.None;
    }

    public void StartGame()
    {
        OnSceneBeforeLoading?.Invoke(this, EventArgs.Empty);
    }

    public void LoadFirstScene()
    {
        StartCoroutine(LoadFirstSceneCoroutine(currentScene));
    }

    IEnumerator LoadFirstSceneCoroutine(SceneLoaderEnum.Scene scene)
    {
        
        OnFirstStartGame?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);
        
        Player.Instance.ChangeScenePosition(playerStartPos);

        currentScene = scene;
        
        SceneLoaderEnum.Region newRegion = GetRegion(currentScene);
        if (currentRegion != newRegion)
        {
            currentRegion = newRegion;
            OnChangedRegion?.Invoke(this, newRegion);
        }

        loadingOperation = SceneManager.LoadSceneAsync(scene.ToString());
        OnSceneLoadingStarted?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeScene(SceneLoaderEnum.Scene scene, Vector2 newPos)
    {
        StartCoroutine(ChangeSceneCoroutine(scene, newPos));
    }

    IEnumerator ChangeSceneCoroutine(SceneLoaderEnum.Scene scene, Vector2 newPos)
    {
        OnSceneBeforeLoading?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);
        
        Player.Instance.ChangeScenePosition(newPos);
        currentScene = scene;
        
        SceneLoaderEnum.Region newRegion = GetRegion(currentScene);
        if (currentRegion != newRegion)
        {
            currentRegion = newRegion;
            OnChangedRegion?.Invoke(this, newRegion);
        }
        Debug.LogError(123);

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

        yield return new WaitForSeconds(0.5f);
        
        OnScenePlay?.Invoke(this, EventArgs.Empty);
    }
    
    public void LoadData(GameData gameData)
    {
        if (Enum.TryParse(gameData.sceneName, out SceneLoaderEnum.Scene scene))
            currentScene = scene;
        else currentScene = SceneLoaderEnum.Scene.FarmScene;
        currentRegion = GetRegion(currentScene);

        playerStartPos = gameData.playerPos;
        
        LoadFirstScene();
    }

    public void SaveData(ref GameData data)
    {
        data.sceneName = currentScene.ToString();
    }
}
