using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : PageUI
{
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private EnergyUI energyUI;

    protected override void Awake()
    {
        base.Awake();
    }

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
