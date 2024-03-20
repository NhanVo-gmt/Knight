using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellData : ScriptableObject
{
    public class SpellParams
    {
        public Transform playerTransform;
        public Transform enemyTransform;
    }
    public abstract void Initialize(SpellParams spellParams);
    public abstract void Activate();
}
