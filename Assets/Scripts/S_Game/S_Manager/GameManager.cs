using System;
using System.Collections;
using System.Collections.Generic;
using Knight.UI;
using UnityEngine;

namespace Knight.Manager
{
    [RequireComponent(typeof(GameManagerInput))]
    public class GameManager : SingletonObject<GameManager>
    {
        public enum GameState
        {
            Running,
            Paused,
        }

        private GameState currentGameState = GameState.Running;
        public Action<GameState> OnChangedGameState;

        private GameManagerInput gameManagerInput;
        
        protected override void Awake()
        {
            base.Awake();
            gameManagerInput = GetComponent<GameManagerInput>();
            
            currentGameState = GameState.Running;
        }

        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (gameManagerInput.menuInput)
            {
                gameManagerInput.UseMenuInput();
                TogglePlayerMenuUI();
            }
        }

        public void TogglePlayerMenuUI()
        {
            if (GameCanvas.Instance.IsOpen())
            {
                ChangeGameState(GameState.Running);
            }
            else
            {
                ChangeGameState(GameState.Paused);
            }
            
            GameCanvas.Instance.TogglePlayerMenuUI();
        }


        public void ChangeGameState(GameState gameState)
        {
            currentGameState = gameState;
            
            switch (currentGameState)
            {
                case GameState.Running:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
                
            }
            
            OnChangedGameState.Invoke(currentGameState);
        }
    }
    
}
