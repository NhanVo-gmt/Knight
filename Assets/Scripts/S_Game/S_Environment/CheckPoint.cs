using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform sitPos;

    public Vector2 GetRestPos()
    {
        Debug.LogError(sitPos.position);
        return sitPos.position;
    }
}
