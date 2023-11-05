using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knight.UI
{
    public class PlayerMenuUI : MonoBehaviour
    {
        [SerializeField] private List<PlayerMenuTabUI> tabList = new List<PlayerMenuTabUI>();
        [SerializeField] private Button prevBtn;
        [SerializeField] private Button nextBtn;
        
        private CanvasGroup canvasGroup;
        private int currentTabIndex = 0;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            
        }

        public void Toggle()
        {
            canvasGroup.alpha = 1 - canvasGroup.alpha;
        }

        public void OpenNextTab()
        {
            currentTabIndex++;
        }

        public void OpenPreviousTab()
        {
            currentTabIndex--;
        }

        void UpdateTab()
        {
            UpdateBtn();
            
        }

        void UpdateBtn()
        {
            prevBtn.enabled = true;
            nextBtn.enabled = true;
            if (currentTabIndex == 0)
            {
                prevBtn.enabled = false;
            }

            if (currentTabIndex == tabList.Count - 1)
            {
                nextBtn.enabled = false;
            }
        }
    }
}
