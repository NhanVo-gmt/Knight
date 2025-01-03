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

        protected override void Awake()
        {
            base.Awake();
            currentState = CanvasState.None;
            playerMenuUI = GetComponentInChildren<PlayerMenuUI>();
        }

        private void Start()
        {
            SceneLoader.Instance.OnSceneBeforeLoading += HideGameUI;
            SceneLoader.Instance.OnSceneReadyToPlay += ShowGameUI;
        }

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

        public bool IsOpen()
        {
            return currentState != CanvasState.None;
        }
    }
    
}
