using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GizmosDrawer : MonoBehaviour
{
    public enum GizmoType {
        None,
        Sphere,
        WireShere,
        Cube,
        WireCube
    }

    [SerializeField] Color color;

    static GizmoType gizmoType;
    static Vector2 _position;
    static float _radius;
    private static Vector2 _size;

    public static void DrawSphere(Vector2 position, float radius) {
        
        _position = position;
        _radius = radius;
        gizmoType = GizmoType.Sphere;
    }

    public static void DrawWireSphere(Vector2 position, float radius)
    {
        _position = position;
        _radius = radius;
        gizmoType = GizmoType.WireShere;
    }
    
    public static void DrawCube(Vector2 position, Vector2 size) {
        
        _position = position;
        _size = size;
        gizmoType = GizmoType.Cube;
    }

    public static void DrawWireCube(Vector2 position, Vector2 size)
    {
        _position = position;
        _size = size;
        gizmoType = GizmoType.WireCube;
    }

    private void OnDrawGizmos() {
        Gizmos.color = color;

        switch (gizmoType) {
            case GizmoType.Sphere:
                Gizmos.DrawSphere(_position, _radius);
                break;
            case GizmoType.WireShere:
                Gizmos.DrawWireSphere(_position, _radius);
                break;
            case GizmoType.Cube:
                Gizmos.DrawCube(_position, _size);
                break;
            case GizmoType.WireCube:
                Gizmos.DrawWireCube(_position, _size);
                break;
        }
    }
}
