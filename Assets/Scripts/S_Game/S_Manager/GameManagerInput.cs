using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Knight.Manager
{
    public class GameManagerInput : MonoBehaviour
    {
        private GameManagerControls gameManagerControls;
        
        public bool menuInput { get; private set; }
        public bool prevTabInput { get; private set; }
        public bool nextTabInput { get; private set; }
        
        private void Awake()
        {
            gameManagerControls = new GameManagerControls();
        }

        private void OnEnable()
        {
            gameManagerControls.Enable();
        }

        private void OnDisable()
        {
            gameManagerControls.Disable();
        }

        private void Start()
        {
            InventoryRegister();
            TabRegister();
        }

        void InventoryRegister()
        {
            gameManagerControls.Global.Menu.started += OnMenuInput;
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
        
        private void OnPrevTabInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ResetInput();
                prevTabInput = true;
            }
        }
        
        private void OnNextTabInput(InputAction.CallbackContext context)
        {
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
