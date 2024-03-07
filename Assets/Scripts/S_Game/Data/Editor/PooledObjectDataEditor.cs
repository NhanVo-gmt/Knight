using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(PooledObjectData), true)]
public class PooledObjectDataEditor : Editor
{
    private PooledObjectData pooledObjectData;

    private void OnEnable()
    {
        pooledObjectData = target as PooledObjectData;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Sprite sprite = null;
        
        VFXData vfxData = pooledObjectData as VFXData;
        if (vfxData != null)
        {
            sprite = vfxData.sprite;
        }
        ItemData itemData = pooledObjectData as ItemData;
        if (itemData != null)
        {
            sprite = itemData.sprite;
        }
        
        if (sprite == null) return;

        Texture2D texture = AssetPreview.GetAssetPreview(sprite);
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
#endif
