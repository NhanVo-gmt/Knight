using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScrollViewAutoScroll : MonoBehaviour
{
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform content;
    [SerializeField] private float transitionDuration = 0.2f;

    private void Update()
    {
        
    }
    
    private class TransitionHelper
    {
        private float duration = 0f;
        private float timeElapse = 0f;
        private float progress = 0f;

        public bool inProgress = false;

        public Vector2 posCurrent;
        private Vector2 posFrom;
        private Vector2 posTo;

        public void Update()
        {
            Tick();
            CalculatePosition();
        }

        private void Tick()
        {
            if (!inProgress) return;

            timeElapse += Time.deltaTime;
            progress = timeElapse / duration;
            if (progress > 1f)
            {
                progress = 1f;
                TransitionComplete();
            }
            
        }

        private void TransitionComplete()
        {
            inProgress = false;
        }
        
        private void CalculatePosition()
        {
            posCurrent = Vector2.Lerp(posFrom, posTo, progress);
        }

        public void Clear()
        {
            duration = 0f;
            timeElapse = 0f;
            progress = 0f;
            inProgress = false;
        }

        public void TransitionPositionFromTo(Vector2 from, Vector2 to, float duration)
        {
            Clear();
            posFrom = from;
            posTo = to;
            this.duration = duration;

            inProgress = true;
        }
    }
}
