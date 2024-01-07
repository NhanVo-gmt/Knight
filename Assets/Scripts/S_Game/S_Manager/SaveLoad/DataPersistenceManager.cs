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

    [SerializeField] private bool canSaveGame = false;

    protected override void Awake()
    {
        base.Awake();
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        FindAllDataPersistenceObjects();
    }

    private void Start()
    {
        SceneLoader.Instance.OnScenePlay += SceneLoader_OnScenePlay;
    }

    private void SceneLoader_OnScenePlay(object sender, EventArgs e)
    {
        canSaveGame = true;
    }


    private void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        
        this.dataPersistenceObjects = new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        
        gameData = new GameData();
        // push the loaded data to all other scripts
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }

    public void LoadGame()
    {
        Debug.Log("Load Game");
        
        // Load saved file
        gameData = fileDataHandler.Load();
        
        // If no data can be loaded, initialize to a new game
        if (gameData == null)
        {
            Debug.Log("No data was found. Please choose new game.");
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
        if (!canSaveGame) return;
        
        Debug.Log("Save Game");
        
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
        // Load saved file
        gameData = fileDataHandler.Load();
        
        return gameData != null;
    }
}
