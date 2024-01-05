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
            gameObject.SetActive(false);
        }

        public void LoadGame()
        {
            if (!DataPersistenceManager.Instance.HasGameData()) return;
            
            DataPersistenceManager.Instance.LoadGame();
            gameObject.SetActive(false); //todo lerp menu
        }
    }
    
}
