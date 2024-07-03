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

    public EventHandler<Vector2> OnChangedPlayerPosition;

    [SerializeField] private SceneLoaderEnum.Scene currentScene = SceneLoaderEnum.Scene.MenuScene;
    [SerializeField] private SceneLoaderEnum.Region currentRegion = SceneLoaderEnum.Region.None;

    private Vector2 playerStartPos = new Vector2(0, 100);

    AsyncOperation loadingOperation;

    protected override void Awake()
    {
        base.Awake();
        currentScene = GetSceneFromUnityScene(SceneManager.GetActiveScene().name);
        currentRegion = GetRegion(currentScene);
        OnChangedRegion?.Invoke(this, currentRegion);
    }

    #region Get Methods
    
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
        
        Debug.LogWarning($"Can not get region from {scene}");
        return SceneLoaderEnum.Region.None;
    }

    #endregion

    #region Load Scene
    
    public void StartGame()
    {
        OnSceneBeforeLoading?.Invoke(this, EventArgs.Empty);
    }

    public void LoadFirstScene(SceneLoaderEnum.Scene scene)
    {
        StartCoroutine(LoadFirstSceneCoroutine(scene));
        
        DataPersistenceManager.Instance.FindAllDataPersistenceObjects();
    }

    IEnumerator LoadFirstSceneCoroutine(SceneLoaderEnum.Scene scene)
    {
        OnFirstStartGame?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);
        
        OnChangedPlayerPosition?.Invoke(this, playerStartPos);

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
        DataPersistenceManager.Instance.SaveGame();
        
        StartCoroutine(ChangeSceneCoroutine(scene, newPos));
    }

    IEnumerator ChangeSceneCoroutine(SceneLoaderEnum.Scene scene, Vector2 newPos)
    {
        OnSceneBeforeLoading?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);
        
        OnChangedPlayerPosition?.Invoke(this, newPos);
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
    
    IEnumerator OnSceneLoadingCompletedCoroutine()
    {
        DataPersistenceManager.Instance.FindAllDataPersistenceObjects();
        DataPersistenceManager.Instance.LoadGame();
        OnSceneLoadingCompleted?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(1f);
        
        OnSceneReadyToPlay?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(0.5f);
        
        OnScenePlay?.Invoke(this, EventArgs.Empty);
    }

    #endregion
    

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
    
    #region Save Load

    public bool IsLoadFirstTime()
    {
        return true;
    }
    
    public void LoadData(GameData gameData)
    {
        playerStartPos = gameData.playerPos;
        if (Enum.TryParse(gameData.sceneName, out SceneLoaderEnum.Scene scene))
        {
            LoadFirstScene(scene);
        }
        else
        {
            Debug.LogError("There is error with data load farm scene");
            LoadFirstScene(SceneLoaderEnum.Scene.FarmScene);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.sceneName = currentScene.ToString();
    }
    

    #endregion
    
}
