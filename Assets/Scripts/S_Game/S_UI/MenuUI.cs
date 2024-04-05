using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private AudioListener listener;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void NewGame()
        {
            StartCoroutine(NewGameCoroutine());
        }

        IEnumerator NewGameCoroutine()
        {
            listener.enabled = false;
            SceneLoader.Instance.StartGame();
            GameSettings.Instance.StartGame();
            canvasGroup.alpha = 0f;
            
            yield return new WaitForSeconds(2f);
            
            DataPersistenceManager.Instance.NewGame();
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
            canvasGroup.alpha = 0f;
            
            yield return new WaitForSeconds(.2f);
            
            DataPersistenceManager.Instance.LoadGame();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
    
}
