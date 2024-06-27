using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.UI
{
    using UnityEngine.UI;

    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private AudioListener listener;
        private CanvasGroup canvasGroup;

        [SerializeField] private Button startBtn;
        [SerializeField] private Button loadBtn;
        [SerializeField] private Button optionsBtn;
        [SerializeField] private Button endBtn;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            startBtn.onClick.RemoveAllListeners();
            loadBtn.onClick.RemoveAllListeners();
            optionsBtn.onClick.RemoveAllListeners();
            endBtn.onClick.RemoveAllListeners();
            
            startBtn.onClick.AddListener(NewGame);
            loadBtn.onClick.AddListener(LoadGame);
            optionsBtn.onClick.AddListener(Quit); // todo options
            endBtn.onClick.AddListener(Quit);
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
            
            DataPersistenceManager.Instance.LoadGame(true);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
    
}
