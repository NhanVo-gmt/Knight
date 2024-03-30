using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleBackgroundEffect
{
    public string Name;
    [Range(0, 1)] public float parallax;
    public Material blurMat;
}

// [CreateAssetMenu(fileName = "BackgroundEffectData", menuName = "ScriptableObjects/BackgroundEffectData")]
public class BackgroundEffectData : ScriptableObject
{
    public SingleBackgroundEffect[] BackgroundEffects;
    
    public enum EffectType
    {
        Parallax,
        Blur
    }
    
    public SingleBackgroundEffect GetEffect(string targetName)
    {
        foreach (SingleBackgroundEffect single in BackgroundEffects)
        {
            if (targetName.ToLower().Equals(single.Name.ToLower()))
            {
                return single;
            }
        }

        return null;
    }
}
