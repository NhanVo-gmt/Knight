using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GizmosDrawer : MonoBehaviour
{
    public class CustomGizmos
    {
        public GizmoType gizmoType;
        public Color color;
        public Vector2 _position;
        public float _radius;
        public Vector2 _size;
    }
    
    public enum GizmoType {
        None,
        Sphere,
        WireShere,
        Cube,
        WireCube
    }

    public static Color color;


    private static CustomGizmos[] gizmosArray = new CustomGizmos[10];
    private static int currentIndex;
    private static int length;
    private static bool hasInit = false;

    public static void Begin()
    {
        currentIndex = 0;
        color = Color.red;

        if (!hasInit)
        {
            hasInit = true;
            for (int i = 0; i < 10; i++)
            {
                gizmosArray[i] = new CustomGizmos();
            }
        }
    }

    public static void End()
    {
        length = currentIndex;
    }

    public static void DrawSphere(Vector2 position, float radius)
    {
        gizmosArray[currentIndex].color = color;
        gizmosArray[currentIndex]._position = position;
        gizmosArray[currentIndex]._radius = radius;
        gizmosArray[currentIndex].gizmoType = GizmoType.Sphere;
        currentIndex++;
    }

    public static void DrawWireSphere(Vector2 position, float radius)
    {
        gizmosArray[currentIndex].color = color;
        gizmosArray[currentIndex]._position = position;
        gizmosArray[currentIndex]._radius = radius;
        gizmosArray[currentIndex].gizmoType = GizmoType.WireShere;
        currentIndex++;
    }
    
    public static void DrawCube(Vector2 position, Vector2 size) {
        
        gizmosArray[currentIndex].color = color;
        gizmosArray[currentIndex]._position = position;
        gizmosArray[currentIndex]._size = size;
        gizmosArray[currentIndex].gizmoType = GizmoType.Cube;
        currentIndex++;
    }

    public static void DrawWireCube(Vector2 position, Vector2 size)
    {
        
        gizmosArray[currentIndex].color = color;
        gizmosArray[currentIndex]._position = position;
        gizmosArray[currentIndex]._size = size;
        gizmosArray[currentIndex].gizmoType = GizmoType.WireCube;
        currentIndex++;
    }

    public static void SetDirty()
    {
        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
    }

    private void OnDrawGizmos()
    {
        if (length == 0) return;
        

        for (int i = 0; i < length; i++)
        {
            CustomGizmos gizmos = gizmosArray[i];
            
            Gizmos.color = gizmos.color;
            
            switch (gizmos.gizmoType)
            {
                case GizmoType.Sphere:
                    Gizmos.DrawSphere(gizmos._position, gizmos._radius);
                    break;
                case GizmoType.WireShere:
                    Gizmos.DrawWireSphere(gizmos._position, gizmos._radius);
                    break;
                case GizmoType.Cube:
                    Gizmos.DrawCube(gizmos._position, gizmos._size);
                    break;
                case GizmoType.WireCube:
                    Gizmos.DrawWireCube(gizmos._position, gizmos._size);
                    break;
            }
        }
    }
}
