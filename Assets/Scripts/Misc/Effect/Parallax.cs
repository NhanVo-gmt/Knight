using System;
using System.Collections;
using System.Collections.Generic;
using Knight.Camera;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parralaxEffect;
    [SerializeField] private Transform startCamPos;
    private Vector2 startPos;
    
    private Camera cam;

    private void Awake()
    {
        startPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dist = GetDistanceFromEffect(cam.transform.position);
        transform.position = new Vector2(startPos.x + dist.x, startPos.y + dist.y);
    }


    Vector2 GetDistanceFromEffect(Vector2 camPos)
    {
        return new Vector2((camPos.x - startCamPos.position.x) * parralaxEffect, (camPos.y - startCamPos.position.y) * parralaxEffect);
    }
}
