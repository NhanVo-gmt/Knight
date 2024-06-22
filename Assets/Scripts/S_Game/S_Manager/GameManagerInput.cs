using System;
using System.Collections;
using System.Collections.Generic;
using Knight.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Knight.Manager
{
    public class GameManagerInput : MonoBehaviour
    {
        private GameManagerControls gameManagerControls;
        
        public bool menuInput      { get; private set; }
        public bool prevTabInput   { get; private set; }
        public bool nextTabInput   { get; private set; }
        public bool closeMenuInput { get; private set; }
        
        private void Awake()
        {
            gameManagerControls = new GameManagerControls();
        }

        private void OnEnable()
        {
            gameManagerControls.Enable();
            
            MenuRegister();
            TabRegister();
        }

        private void OnDisable()
        {
            gameManagerControls.Disable();
        }

        void MenuRegister()
        {
            gameManagerControls.Global.Menu.started      += OnMenuInput;
            gameManagerControls.Global.CloseMenu.started += OnCloseMenuInput;
        }
        
        void TabRegister()
        {
            gameManagerControls.Global.PrevTab.started += OnPrevTabInput;
            gameManagerControls.Global.NextTab.started += OnNextTabInput;
        }

        private void OnMenuInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ResetInput();
                menuInput = true;
            }
        }

        private void OnCloseMenuInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ResetInput();
                closeMenuInput = true;
            }
        }
        
        private void OnPrevTabInput(InputAction.CallbackContext context)
        {
            if (!GameCanvas.Instance || !GameCanvas.Instance.IsOpen()) return;
            
            if (context.started)
            {
                ResetInput();
                prevTabInput = true;
            }
        }
        
        private void OnNextTabInput(InputAction.CallbackContext context)
        {
            if (!GameCanvas.Instance || !GameCanvas.Instance.IsOpen()) return;
            
            if (context.started)
            {
                ResetInput();
                nextTabInput = true;
            }
        }

        public void UseMenuInput()
        {
            menuInput = false;
        }

        public void UseCloseMenuInput()
        {
            closeMenuInput = false;
        }
        
        public void UsePrevTabInput()
        {
            prevTabInput = false;
        }
        
        public void UseNextTabInput()
        {
            nextTabInput = false;
        }

        void ResetInput()
        {
            menuInput = false;
            prevTabInput = false;
            nextTabInput = false;
        }
    }
    
}
