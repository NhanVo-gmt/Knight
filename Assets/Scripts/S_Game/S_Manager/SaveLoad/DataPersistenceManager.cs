using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : SingletonObject<DataPersistenceManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    
    
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;

    protected override void Awake()
    {
        base.Awake();
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        // dataPersistenceObjects = FindAllDataPersistenceObjects();
        // LoadGame();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Load new scene");
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        Debug.Log("Save old scene");
        SaveGame();
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load saved file
        gameData = fileDataHandler.Load();
        
        // If no data can be loaded, initialize to a new game
        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            return;
        }
        
        // push the loaded data to all other scripts
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // Pass data to other scripts so they can update
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }
        
        // Save to file
        fileDataHandler.Save(gameData);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
