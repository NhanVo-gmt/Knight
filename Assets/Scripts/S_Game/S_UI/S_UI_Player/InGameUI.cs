using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private EnergyUI energyUI;

    private void Start()
    {
        GameSettings.Instance.OnGameInitialized += InitUI;
    }

    private void InitUI()
    {
        healthUI.gameObject.SetActive(true);
        energyUI.gameObject.SetActive(true);
    }
}
