using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField]
    private Image fuelBar;

    private Animator animator;

    private float targetFuel = 100;

    private void Start()
    {
        Player.onPlayerFuelChange += HandlePlayerFuelChange;
        GameOverUI.onRestartClick += HandleRestartClick;
        animator = GetComponent<Animator>();
        ResetFuelBar();
    }

    private void OnDestroy()
    {
        Player.onPlayerFuelChange -= HandlePlayerFuelChange;
        GameOverUI.onRestartClick -= HandleRestartClick;
    }

    private void Update()
    {
        UpdateFillPercent(
            Mathf.Lerp(fuelBar.material.GetFloat("_Fill_percent"), targetFuel, Time.deltaTime * 5)
        );
    }

    private void HandlePlayerFuelChange(object sender, Player.onPlayerFuelChangeEventArgs e)
    {
        targetFuel = (float)e.fuel / 100;
        animator.SetBool("IsLowFuel", targetFuel < 0.3);
    }

    private void HandleRestartClick(object sender, EventArgs e)
    {
        ResetFuelBar();
    }

    private void ResetFuelBar()
    {
        targetFuel = 100;
        UpdateFillPercent(1);
    }

    private void UpdateFillPercent(float fillAmount)
    {
        fuelBar.material.SetFloat("_Fill_percent", fillAmount);
    }
}
