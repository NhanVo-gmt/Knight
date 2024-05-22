using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.UI
{
    public class GameCanvas : SingletonObject<GameCanvas>
    {
        public enum CanvasState
        {
            None,
            Menu,
            Shop
        }
        
        public CanvasState currentState { get; private set; }
        
        [SerializeField] CanvasGroup inGameUI;
        private PlayerMenuUI playerMenuUI;
        private LoadingUI loadingUI;

        protected override void Awake()
        {
            base.Awake();
            currentState = CanvasState.None;
            playerMenuUI = GetComponentInChildren<PlayerMenuUI>();
            loadingUI = GetComponentInChildren<LoadingUI>();
        }

        private void OnEnable()
        {
            SceneLoader.Instance.OnSceneBeforeLoading += HideGameUI;
            SceneLoader.Instance.OnSceneReadyToPlay += ShowGameUI;
        }

        private void OnDisable()
        {
            SceneLoader.Instance.OnSceneBeforeLoading -= HideGameUI;
            SceneLoader.Instance.OnSceneReadyToPlay -= ShowGameUI;
        }

        #region In Game UI

        private void HideGameUI(object sender, EventArgs e)
        {
            inGameUI.alpha = 0f;
            //todo dialogue
        }
        
        private void ShowGameUI(object sender, EventArgs e)
        {
            inGameUI.alpha = 1f;
            //todo dialogue
        }

        #endregion


        #region Menu UI

        public void TogglePlayerMenuUI()
        {
            if (currentState == CanvasState.None)
            {
                currentState = CanvasState.Menu;
                playerMenuUI.Toggle();
            }
            else if (currentState == CanvasState.Menu)
            {
                currentState = CanvasState.None;
                playerMenuUI.Toggle();
            }
        }

        public void OpenNextTabPlayerMenu()
        {
            playerMenuUI.OpenNextTab();
        }
        
        public void OpenPrevTabPlayerMenu()
        {
            playerMenuUI.OpenPreviousTab();
        }

        #endregion


        public bool IsOpen()
        {
            return currentState != CanvasState.None;
        }

        #region Loading UI


        #endregion

    }
    
}
