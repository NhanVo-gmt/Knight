using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Camera
{
    public class CameraController : SingletonObject<CameraController>
    {
        Transform cam;

        protected override void Awake() 
        {
            base.Awake();
            cam = GetComponent<Transform>();
        }
        
        void LateUpdate() 
        {
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
