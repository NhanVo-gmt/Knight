using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] bool NeedToResetPlayerPosition;

    [SerializeField] AttackData attackData;

    void Awake() 
    {
        attackData = FindObjectOfType<GameSettings>().TrapSettings;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(attackData, IDamageable.DamagerType.Trap);
        }
    }
}
