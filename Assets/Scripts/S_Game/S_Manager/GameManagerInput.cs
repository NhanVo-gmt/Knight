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
        }

        void InventoryRegister()
        {
            gameManagerControls.Global.Menu.started += OnMenuInput;
        }

        private void OnMenuInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                menuInput = true;
            }
        }

        public void UseMenuInput()
        {
            menuInput = false;
        }
    }
    
}
