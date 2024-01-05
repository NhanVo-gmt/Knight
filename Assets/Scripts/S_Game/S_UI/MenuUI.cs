using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.UI
{
    public class MenuUI : MonoBehaviour
    {
        public void NewGame()
        {
            DataPersistenceManager.Instance.NewGame();
            SceneLoader.Instance.ChangeScene(SceneLoader.Scene.FarmScene, Vector2.zero); //todo load player position
        }

        public void LoadGame()
        {
            if (!DataPersistenceManager.Instance.HasGameData()) return;
            DataPersistenceManager.Instance.LoadGame();
            SceneLoader.Instance.ChangeScene(SceneLoader.Scene.FarmScene, Vector2.zero); //todo load player position
        }
    }
    
}
