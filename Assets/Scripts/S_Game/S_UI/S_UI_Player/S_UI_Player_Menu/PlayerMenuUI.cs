using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

namespace Knight.UI
{
    public class PlayerMenuUI : PageUI
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
        
        [SerializeField] [ReadOnlyInspector] private int currentTabIndex = 0;

        protected override void Awake()
        {
            base.Awake();
            canvasGroup.alpha = 0;
        }

        public override void Show()
        {
            isOpened = true;
            StartCoroutine(ShowCoroutine());
        }

        IEnumerator ShowCoroutine()
        {
            CloseAllTabs();
            yield return canvasGroup.Fade(1 - canvasGroup.alpha, 0.1f);
            
            currentTabIndex = 0;
            OpenTab(0);
        }

        public override void Hide()
        {
            CloseAllTabs();
            base.Hide();
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
