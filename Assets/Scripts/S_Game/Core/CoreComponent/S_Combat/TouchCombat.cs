using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCombat : MonoBehaviour
{
    Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == col) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(1, IDamageable.DamagerTarget.Enemy, Vector2.zero); //hardcode
        }
    }
}
