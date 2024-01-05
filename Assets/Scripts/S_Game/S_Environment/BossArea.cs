using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossArea : MonoBehaviour, IDataPersistence
{
    public enum Boss
    {
        Wildboar,
        HeadlessKnight
    }

    [SerializeField] private Boss boss;
    [SerializeField] private Health bossHealth;
    [SerializeField] private Collider2D[] walls;

    private void OnEnable()
    {
        if (bossHealth)
            bossHealth.OnDie += OnBossDie;
    }

    private void OnDisable()
    {
        if (bossHealth)
            bossHealth.OnDie -= OnBossDie;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!bossHealth) return;
        
        if (other.CompareTag("Player"))
        {
            foreach (Collider2D wall in walls)
            {
                wall.isTrigger = false;
            }
        }
    }

    private void OnBossDie()
    {
        foreach (Collider2D wall in walls)
        {
            wall.isTrigger = true;
        }
    }

    public void LoadData(GameData gameData)
    {
        foreach (string bossStr in gameData.bossDefeated)
        {
            if (bossStr.Equals(boss.ToString()))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.bossDefeated.Add(boss.ToString());
    }
}
