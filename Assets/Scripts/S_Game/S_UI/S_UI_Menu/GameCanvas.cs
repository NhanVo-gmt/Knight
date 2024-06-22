using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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

        private static CanvasState         currentState;
        public         Action<CanvasState> OnChangeState;

        private static Dictionary<Type, PageUI> TypeToPages = new();

        protected override void Awake()
        {
            base.Awake();
            currentState = CanvasState.None;
            
            PopulatePages();
        }

        private void Update()
        {
            Debug.Log(currentState);
        }

        void PopulatePages()
        {
            foreach (PageUI page in GetComponentsInChildren<PageUI>())
            {
                TypeToPages[page.GetType()] = page;
            }
        }

        private void OnEnable()
        {
            SceneLoader.Instance.OnSceneBeforeLoading += HideGameUI;
            SceneLoader.Instance.OnSceneReadyToPlay += ShowGameUI;
            
            foreach (PageUI page in TypeToPages.Values)
            {
                page.OnOpen += ChangePage;
            }
        }

        private void OnDisable()
        {
            SceneLoader.Instance.OnSceneBeforeLoading -= HideGameUI;
            SceneLoader.Instance.OnSceneReadyToPlay -= ShowGameUI;
            
            foreach (PageUI page in TypeToPages.Values)
            {
                page.OnOpen -= ChangePage;
            }
        }

        private void ChangePage(PageUI page)
        {
            if (page is PlayerMenuUI)
            {
                ChangeState(CanvasState.Menu);
            }
            else if (page is ShopUI)
            {
                ChangeState(CanvasState.Shop);
            }
            else if (page is InGameUI)
            {
                ChangeState(CanvasState.None);
            }
        }


        public static void ShowPage<T>() where T : PageUI
        {
            Type pageType = typeof(T);
            PageUI page = TypeToPages[pageType];
            
            page.Show();
        }
        
        public static void HidePage<T>() where T : PageUI
        {
            Type pageType = typeof(T);
            PageUI page = TypeToPages[pageType];
            
            page.Hide();
        }
        
        public static void TogglePage<T>() where T : PageUI
        {
            Type pageType = typeof(T);
            PageUI page = TypeToPages[pageType];
            
            page.Toggle();
        }

        public static T GetPage<T>() where T : PageUI
        {
            return TypeToPages[typeof(T)] as T;
        }

        public static void CloseAllPage()
        {
            foreach (PageUI page in TypeToPages.Values)
            {
                if (page is InGameUI)
                {
                    page.Show();
                    continue;
                }
                
                page.Hide();
            }
        }

        void ChangeState(CanvasState newState)
        {
            currentState = newState;
            OnChangeState?.Invoke(currentState);
        }

        #region Event

        private void HideGameUI(object sender, EventArgs e)
        {
            HidePage<InGameUI>();
            HidePage<DialogueUI>();
        }
        
        private void ShowGameUI(object sender, EventArgs e)
        {
            ShowPage<InGameUI>();
            ShowPage<DialogueUI>();
        }

        #endregion
        


        public bool IsOpen()
        {
            return currentState != CanvasState.None;
        }

    }
    
}
