using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] bool NeedToResetPlayerPosition;
    private readonly int damage = 1;
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Combat>(out Combat target) && target.GetDamagerType() == IDamageable.DamagerTarget.Player)
        {
            target.TakeDamage(damage, IDamageable.DamagerTarget.Trap, Vector2.up, NeedToResetPlayerPosition); 
        }
    }
}
