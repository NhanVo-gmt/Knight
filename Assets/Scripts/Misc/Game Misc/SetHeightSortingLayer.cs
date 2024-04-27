using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetHeightSortingLayer : MonoBehaviour
{
    public SortingLayer sortingLayer;
    
    [ContextMenu("Set From Up Sorting Layer")]
    public void SetUpSortingLayer()
    {
        SpriteRenderer[] allchildSprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in allchildSprites)
        {
            sprite.sortingLayerID = sortingLayer.id;
            sprite.sortingOrder = 1000 - Mathf.RoundToInt(sprite.transform.position.y * 100);
        }
    }
    
    [ContextMenu("Set From Down Sorting Layer")]
    public void SetDownSortingLayer()
    {
        SpriteRenderer[] allchildSprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in allchildSprites)
        {
            sprite.sortingLayerID = sortingLayer.id;
            sprite.sortingOrder = Mathf.RoundToInt(sprite.transform.position.y * 100);
        }
    }
}

