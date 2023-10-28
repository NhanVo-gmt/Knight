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
    }

    [SerializeField] Color color;

    static GizmoType gizmoType;
    static Vector2 _position;
    static float _radius;

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

    private void OnDrawGizmos() {
        Gizmos.color = color;

        switch (gizmoType) {
            case GizmoType.Sphere:
                Gizmos.DrawSphere(_position, _radius);
                break;
            case GizmoType.WireShere:
                Gizmos.DrawWireSphere(_position, _radius);
                break;
            case GizmoType.Cube: // todo
                Gizmos.DrawSphere(_position, _radius);
                break;
        }
    }
}
