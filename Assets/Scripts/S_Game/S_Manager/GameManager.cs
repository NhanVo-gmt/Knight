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
            if (gameManagerInput.prevTabInput)
            {
                gameManagerInput.UsePrevTabInput();
                GameCanvas.Instance.OpenPrevTabPlayerMenu();
            }
            if (gameManagerInput.nextTabInput)
            {
                gameManagerInput.UseNextTabInput();
                GameCanvas.Instance.OpenNextTabPlayerMenu();
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

        #region General Methods
        
        public void Sleep(float duration)
        {
            StartCoroutine(PerformSleep(duration, null, null));
        }

        public void Sleep(float duration, Action methodsBeforeSleep, Action methodsAfterSleep)
        {
            StartCoroutine(PerformSleep(duration, methodsBeforeSleep, methodsAfterSleep));
        }


        IEnumerator PerformSleep(float duration, Action methodsBeforeSleep, Action methodsAfterSleep)
        {
            methodsBeforeSleep?.Invoke();
            
            yield return new WaitForSeconds(0.01f);
            
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1f;
            
            methodsAfterSleep?.Invoke();
        }

        #endregion
    }
    
}
