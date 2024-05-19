using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Knight.UI
{
    public class PlayerMenuUI : MonoBehaviour
    {
        public enum Tab
        {
            Character,
            Inventory,
            Skills,
        }
        
        [Serializable]
        public class PlayerMenuTabUI
        {
            public Tab tab;
            public GameObject tabGO;
        }
        
        [SerializeField] private List<PlayerMenuTabUI> tabList = new List<PlayerMenuTabUI>();
        [SerializeField] private TextMeshProUGUI prevTabText;
        [SerializeField] private TextMeshProUGUI currentTabText;
        [SerializeField] private TextMeshProUGUI nextTabText;
        
        private CanvasGroup canvasGroup;
        private int currentTabIndex = 0;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        public void Toggle()
        {
            StartCoroutine(ToggleCoroutine());
        }

        IEnumerator ToggleCoroutine()
        {
            CloseAllTabs();
            yield return canvasGroup.Fade(1 - canvasGroup.alpha, 0.1f);
            
            currentTabIndex = 0;
            OpenTab(0);
        }

        public void OpenNextTab()
        {
            OpenTab(currentTabIndex + 1);
        }

        public void OpenPreviousTab()
        {
            OpenTab(currentTabIndex - 1);
        }

        public void OpenTab(int index)
        {
            if (index < 0 || index >= tabList.Count) return;
            
            CloseCurrentTab();
            currentTabIndex = index;
            UpdateTab();
        }

        void CloseCurrentTab()
        {
            tabList[currentTabIndex].tabGO.SetActive(false);
        }

        void CloseAllTabs()
        {
            for (int i = 0; i < tabList.Count; i++)
            {
                tabList[i].tabGO.SetActive(false);
            }
        }

        void UpdateTab()
        {
            tabList[currentTabIndex].tabGO.SetActive(true);

            if (currentTabIndex > 0)
            {
                prevTabText.text = tabList[currentTabIndex - 1].tab.ToString();
            }
            else prevTabText.text = String.Empty;
            
            if (currentTabIndex < tabList.Count - 1)
            {
                nextTabText.text = tabList[currentTabIndex + 1].tab.ToString();
            }
            else nextTabText.text = String.Empty;

            currentTabText.text = tabList[currentTabIndex].tab.ToString();
        }

        
    }
}
