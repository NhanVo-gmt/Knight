using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChildSpriteMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material changedMaterial;


    [ContextMenu("Change Child Sprite Material")]
    public void ChangeChildSpriteMaterial()
    {
        foreach(SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.material = changedMaterial;
        }
    }
}
