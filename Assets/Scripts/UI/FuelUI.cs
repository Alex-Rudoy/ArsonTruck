using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField]
    private Image fuelBar;

    private float targetFuel = 100;

    private void Start()
    {
        Player.onPlayerFuelChange += HandlePlayerFuelChange;
        GameOverUI.onRestartClick += HandleRestartClick;
        ResetFuelBar();
    }

    private void Update()
    {
        fuelBar.fillAmount = Mathf.Lerp(fuelBar.fillAmount, targetFuel, Time.deltaTime * 5);
    }

    private void HandlePlayerFuelChange(object sender, Player.onPlayerFuelChangeEventArgs e)
    {
        targetFuel = (float)e.fuel / 100;
    }

    private void HandleRestartClick(object sender, EventArgs e)
    {
        ResetFuelBar();
    }

    private void ResetFuelBar()
    {
        targetFuel = 100;
        fuelBar.fillAmount = 1;
    }
}
