using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonAnimation : MonoBehaviour
{
    [SerializeField] private Vector2 toPos;
    private float speed = 3;

    private RectTransform currentRect;
    private Vector2 startPos;
    
    private void Awake()
    {
        currentRect = GetComponent<RectTransform>();
        startPos = currentRect.rect.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        
    }
}
