using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parralaxEffect;
    private float startPos;
    private Camera cam;

    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (cam.transform.position.x * parralaxEffect);
        transform.position = new Vector2(startPos + dist, transform.position.y);
    }
}
