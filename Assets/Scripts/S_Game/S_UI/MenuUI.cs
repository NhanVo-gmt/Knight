using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.UI
{
    public class MenuUI : MonoBehaviour
    {
        public void NewGame()
        {
            StartCoroutine(NewGameCoroutine());
        }

        IEnumerator NewGameCoroutine()
        {
            SceneLoader.Instance.StartGame();
            GameSettings.Instance.StartGame();
            
            yield return new WaitForSeconds(1f);
            
            DataPersistenceManager.Instance.NewGame();
            gameObject.SetActive(false);
        }

        public void LoadGame()
        {
            if (!DataPersistenceManager.Instance.HasGameData()) return;

            StartCoroutine(LoadGameCoroutine());
        }
        
        IEnumerator LoadGameCoroutine()
        {
            SceneLoader.Instance.StartGame();
            GameSettings.Instance.StartGame();
            
            yield return new WaitForSeconds(1f);
            
            DataPersistenceManager.Instance.LoadGame();
            gameObject.SetActive(false); //todo lerp menu
        }
    }
    
}
